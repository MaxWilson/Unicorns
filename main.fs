module TeaganSquish.Main

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.PIXI
open Fable.Import.Browser

let render = Globals.autoDetectRenderer(window.innerHeight, window.innerWidth, [BackgroundColor (float 0x1099bb); Resolution 1.])
             |> unbox<SystemRenderer>

let gameDiv = document.getElementById("content")
gameDiv.appendChild(render.view) |> ignore

let stage = Container()
let pic = Sprite.fromImage("Unicorn.png")
pic.anchor.x <- 0.5
pic.anchor.y <- 0.5
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
countingText.position.y <- 320.
countingText.anchor.x <- 0.5
stage.addChild(countingText) |> ignore

let rec animate(dt:float) =
    window.requestAnimationFrame(FrameRequestCallback animate) |> ignore
    pic.position.x <- (pic.position.x + 10.)
    if pic.position.x > 1700. then
        pic.position.x <- -600.
    countingText.text <- sprintf "Xpos: %d" (int pic.position.x)
    render.render(stage)

animate 0.
