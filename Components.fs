module Components

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Browser
open Fable.Import.PIXI
open Fable.Import.React
open Fable.Import.React_Extensions

module R = Fable.Helpers.React
open R.Props

let mutable speed = 1
// create a PIXI box with an animation loop
type DisplayBox() =
    inherit React.Component<unit, unit>()
    let mutable animate_id = 0.
    let mutable canvas = null
    let renderer = Globals.autoDetectRenderer(1366., 768., [RendererOptions.BackgroundColor (float 0x1099bb); Resolution 1.; Transparent true]) |> unbox<SystemRenderer>
    member this.render() =
        R.div [ClassName "game-canvas-container"; Ref (fun x -> canvas <- (x :?> HTMLElement))][]
    member this.componentDidMount() =
        canvas.appendChild(renderer.view) |> ignore
        let stage = Container()
        let pic = Container()
        let p = Sprite.fromImage("http://www.goodboydigital.com/pixijs/pixi_v3_github-pad.png")
        p.anchor.x <- 0.5
        p.anchor.y <- 0.5
        pic.addChild(p) |> ignore
        pic.addChild(Text("captcha!",[TextStyle.Align "center"; TextStyle.Font "bold italic 80px"; TextStyle.Fill (U2.Case1 "blue")])) |> ignore
        pic.position.x <- -600.
        pic.position.y <- 300.
        stage.addChild(pic) |> ignore

        let countingText =
            Fable.Import.PIXI.Text("Xpos: 0",
                [
                TextStyle.Font "bold italic 60px Arial"
                TextStyle.Fill (U2.Case1 "#3e1707")
                TextStyle.Align "center"
                TextStyle.Stroke (U2.Case1 "#a4410e")
                TextStyle.StrokeThickness 7.
                ])
        countingText.position.x <- 310.
        countingText.position.y <- 620.
        countingText.anchor.x <- 0.5
        stage.addChild(countingText) |> ignore
        stage.interactive <- true
        stage.on_click (fun e -> speed <- speed + 5) |> ignore

        let rec animate(dt:float) =
            animate_id <- window.requestAnimationFrame(FrameRequestCallback animate)
            pic.position.x <- (pic.position.x + (float speed))
            if pic.position.x > 1700. then
                pic.position.x <- -600.
            countingText.text <- sprintf "Xpos: %d" (int pic.position.x)
            renderer.render(stage)

        animate 0. // start a pixi animation loop
    member this.componentWillUnmount() =
        canvas.removeChild(renderer.view) |> ignore
        window.cancelAnimationFrame animate_id

type App() =
    inherit Component<unit, unit>()
    member this.render() =
        R.div [] [
            unbox "Hello world"
            R.com<DisplayBox,_,_>()[]
            R.button [OnClick (fun (x: MouseEvent) -> speed <- (speed * 2); if speed > 100 then speed <- 1)][R.str "clickme"]
            ]