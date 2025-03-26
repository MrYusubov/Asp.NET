const url = "https://localhost:7149/";

const connection = new signalR.HubConnectionBuilder()
    .withUrl(url + "offers")
    .configureLogging(signalR.LogLevel.Information)
    .build();

let lastBidder = null;
let timer = null;
let currentPrice = 0;
let auctionActive = true;
let countdown = 10;
let countdownInterval = null;

const countdownElement = document.createElement('div');
countdownElement.id = 'countdown';
countdownElement.style.cssText = `
    font-size: 1.5em;
    color: #ff5722;
    font-weight: bold;
    margin: 10px 0;
    padding: 8px 15px;
    background: #f5f5f5;
    border-radius: 20px;
    display: inline-block;
    box-shadow: 0 2px 5px rgba(0,0,0,0.2);
`;
document.querySelector('body').appendChild(countdownElement);

async function start() {
    try {
        await connection.start();
        $.get(url + "api/Offer", function(data) {
            currentPrice = data;
            updatePriceDisplay(data);
        });
        console.log("SignalR Started");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

function updatePriceDisplay(price) {
    document.querySelector("#offerValue").innerHTML = "Current price: " + price + "$ ";
}

function updateCountdownDisplay() {
    countdownElement.textContent = `Time left: ${countdown}s`;
    if(countdown > 5) {
        countdownElement.style.color = '#4CAF50';
    } else if(countdown > 2) {
        countdownElement.style.color = '#FFC107';
    } else {
        countdownElement.style.color = '#F44336';
        countdownElement.style.animation = 'blink 0.5s infinite';
    }
}

function startCountdown() {
    clearInterval(countdownInterval);
    countdown = 10;
    updateCountdownDisplay();
    
    countdownInterval = setInterval(() => {
        countdown--;
        updateCountdownDisplay();
        
        if(countdown <= 0) {
            clearInterval(countdownInterval);
            endAuction();
        }
    }, 1000);
}

connection.on("ReceiveMessage", (username, offer) => {
    currentPrice = offer;
    lastBidder = username;
    updatePriceDisplay(offer);
    
    clearTimeout(timer);
    updateBidButtonState(username);
    startCountdown();
    
    timer = setTimeout(endAuction, 10000);
});

function updateBidButtonState(username) {
    const userInput = document.querySelector("#user").value;
    document.querySelector("#bitBtn").disabled = (userInput === username);
}

function endAuction() {
    clearInterval(countdownInterval);
    countdownElement.textContent = "Auction ended!";
    countdownElement.style.color = '#9C27B0';
    countdownElement.style.animation = 'none';
    
    document.querySelector("#responseOfferValue").innerHTML = 
        `🏆 Winner: ${lastBidder} with ${currentPrice}$`;
    disableAllBidding();
}

function disableAllBidding() {
    auctionActive = false;
    document.querySelector("#bitBtn").disabled = true;
}

async function IncreaseOffer() {
    const user = document.querySelector("#user").value;
    if (!user) {
        alert("Please enter your username!");
        return;
    }
    
    if (!auctionActive) {
        alert("Auction has ended!");
        return;
    }

    document.querySelector("#bitBtn").disabled = true;
    
    try {
        await $.get(url + "api/Offer/Increase?data=100");
        const newPrice = await $.get(url + "api/Offer");
        connection.invoke("SendMessage", user, newPrice);
    } catch (error) {
        console.error("Bid error:", error);
        document.querySelector("#bitBtn").disabled = false;
    }
}

const style = document.createElement('style');
style.textContent = `
    @keyframes blink {
        0% { opacity: 1; }
        50% { opacity: 0.5; }
        100% { opacity: 1; }
    }
`;
document.head.appendChild(style);

start();