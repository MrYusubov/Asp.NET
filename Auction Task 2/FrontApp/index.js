// const url="https://localhost:7149/";

// const connection = new signalR.HubConnectionBuilder()
//     .withUrl(url+"offers")
//     .configureLogging(signalR.LogLevel.Information)
//     .build();

// async function start(){
//     try {
//         await connection.start();

//         $.get(url+"api/Offer",function(data,status){
//             const element=document.querySelector("#offerValue");
//             element.innerHTML="Begin price : "+data+"$ ";
//         })

//         console.log("SignalR Started");
//     } catch (err) {
//         console.log(err);
//         setTimeout(() => {
//             start();
//         }, 5000);
//     }
// }

// start();

// const element1=document.querySelector("#info");
// const element2=document.querySelector("#disconnectInfo");
// connection.on("ReceiveConnectInfo",(message)=>{
//     // element1.innerHTML="</br>"+message;
//     // element2.innerHTML="";
// })


// connection.on("ReceiveDisconnectInfo",(message)=>{
//     // element2.innerHTML=message;
//     // element1.innerHTML="";
// })

// connection.on("ReceiveMessage",(message,data)=>{
//     let element=document.querySelector("#responseOfferValue");
//     element.innerHTML=message+data+"$ ";
// })

// async function IncreaseOffer(){
//     let user=document.querySelector("#user");

//     $.get(url+"api/Offer/Increase?data=100",function(data,status){
//         $.get(url+"api/Offer",function(data,status){
//             connection.invoke("SendMessage",user.value,data);
//         })
//     })
// }



const url = "https://localhost:7149/";
var CURRENT_ROOM = "";
var currentUser = "";
var room = document.querySelector("#room");
var element = document.querySelector("#offerValue");
var button = document.querySelector("#offerBtn");


const connection = new signalR.HubConnectionBuilder()
    .withUrl(url + "offers")
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();

        $.get(url + "api/Offer/Room?room=" + CURRENT_ROOM, function (data, status) {
            element.innerHTML = "Begin price : " + data + "$";
        })


        console.log("SignalR Started");
    } catch (err) {
        console.log(err);
        setTimeout(() => {
            start();
        }, 5000);
    }
}

async function JoinRoom(roomName) {
    CURRENT_ROOM = roomName;
    room.style.display = "block";
    await start();

    currentUser = document.querySelector("#user").value;

    await connection.invoke("JoinRoom", CURRENT_ROOM, currentUser);
    const btn=document.getElementById("joinBtn");
    const leaveBtn=document.getElementById("leaveBtn");
    leaveBtn.style="display:block";
    btn.style="display:none";
}

async function LeaveRoom() {
    if (!CURRENT_ROOM) return;

    await connection.invoke("LeaveRoom", CURRENT_ROOM,currentUser);
    
    room.style.display = "none";
    CURRENT_ROOM = "";

    const btn = document.getElementById("joinBtn");
    const leaveBtn = document.getElementById("leaveBtn");
    leaveBtn.style.display = "none";
    btn.style.display = "block";
}
connection.on("RoomFull", (room) => {
    alert(`"${room}"`);
});
connection.on("UserLeft", (user) => {
    let infoUser = document.querySelector("#info");
    infoUser.innerHTML = `${user} left room`;
});

connection.on("ReceiveJoinInfo", (message) => {
    let infoUser = document.querySelector("#info");
    infoUser.innerHTML = message + " connected to our room";
})

async function IncreaseOffer() {
    const user = document.querySelector("#user");

    $.get(
        url + `api/Offer/IncreaseRoom?room=${CURRENT_ROOM}&data=1000`,
        function (data, status) {
            $.get(
                url + "api/Offer/Room?room=" + CURRENT_ROOM,
                async function (data, status) {
                    var element2 = document.querySelector("#offerValue2");
                    element2.innerHTML = data;

                    await connection.invoke("SendMessageRoom", CURRENT_ROOM, user.value);
                }
            )
        }
    )
}

connection.on("ReceiveInfoRoom", (user, data) => {
    var element2 = document.querySelector("#offerValue2");
    element2.innerHTML = user + ` offer this price ${data}$`;
});