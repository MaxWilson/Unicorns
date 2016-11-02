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
let pic = Sprite.fromImage("unicorn.png")
pic.anchor.x <- 0.5
pic.anchor.y <- 0.5
pic.position.x <- -1000.
pic.position.y <- 300.
stage.addChild(pic) |> ignore

let rec animate(dt:float) =
    window.requestAnimationFrame(FrameRequestCallback animate) |> ignore
    pic.position.x <- (pic.position.x + 5.)
    if pic.position.x > 1800. then
        pic.position.x <- -1000.
    render.render(stage)

animate 0.
