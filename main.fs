module TeaganSquish.Main

open System
open Fable.Core
open Fable.Import

module R = Fable.Helpers.React

// Check components.fs to see how to build React components from F#
open Components


ReactDom.render(
    R.com<App,_,_> () [],
    Browser.document.getElementById "content")
|> ignore