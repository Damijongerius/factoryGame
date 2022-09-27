export class ErrorHandler{

    OnConnection(err, result){
        console.log("something went wrong while connecting");
    }

    OnInsert(err, result){
        console.log("couldn't insert");
    }
    
}