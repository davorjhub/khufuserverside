namespace KhufuServer

open MongoDB.Bson
module Controller =

    open Microsoft.AspNetCore.Http
    open FSharp.Control.Tasks.V2.ContextInsensitive
    open Giraffe
    open KhufuServer.Models

    //Get
    let getTickets =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let response = MongoCRUD.readAll 
                return! json response next ctx
            }
    

    let getTicketFromId (id : string)=
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let objid = BsonObjectId(ObjectId.Parse(id))
                let response = MongoCRUD.readOnId(objid) 
                if response.ToJson() = "[]" then return! text("400") next ctx
                else return! json response next ctx
            }

    //post
    let createTicket=
        fun (next : HttpFunc) (ctx : HttpContext)  ->
             task {
                let! ticketToInsert = ctx.BindJsonAsync<Ticket>()
                let ticketToInsert = {ticketToInsert with Id = BsonObjectId(ObjectId.GenerateNewId())}
                let response = MongoCRUD.create ticketToInsert 
                return! json response next ctx 
            } 

    

