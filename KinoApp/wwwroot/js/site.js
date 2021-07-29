var selectedNumbers = [];
var numberOfNumbers = null;
var numberOfDraws = null;
var betAmount = null;

$(document).ready(function () {
    $('.alert-success').delay(2000).slideUp('slow');
});

function dismissAlert() {
    $('.alert-danger').slideUp('slow');
}

function markNumberAsSelected(cellId) {

    var numberCell = $('#' + cellId);
    var number = numberCell.text().trim();
    if (numberCell.hasClass("selected-number")) {
        numberCell.removeClass("selected-number");
        var index = selectedNumbers.indexOf(number);
        if (index !== -1) {
            selectedNumbers.splice(index, 1);
        }
    }
    else {
        numberCell.addClass("selected-number");
        selectedNumbers.push(number);
    }
}

function markNumberOfNumbersAsSelected(cellId) {
    var numberCell = $('#' + cellId);
    var number = numberCell.text().trim();

    if (numberCell.hasClass("selected-number")) {
        numberCell.removeClass("selected-number");
        numberOfNumbers = null;
    }
    else {
        if (numberOfNumbers != null) {
            $('#number-of-numbers-' + numberOfNumbers + '-cell').removeClass("selected-number");
        }
        numberCell.addClass("selected-number");
        numberOfNumbers = number;
    }
}

function markNumberOfDrawsAsSelected(cellId) {
    var numberCell = $('#' + cellId);
    var number = numberCell.text().trim();

    if (numberCell.hasClass("selected-number")) {
        numberCell.removeClass("selected-number");
        numberOfDraws = null;
    }
    else {
        if (numberOfDraws != null) {
            $('#number-of-draws-' + numberOfDraws + '-cell').removeClass("selected-number");
        }
        numberCell.addClass("selected-number");
        numberOfDraws = number;
    }
}

function markBetAmountAsSelected(cellId) {
    var numberCell = $('#' + cellId);
    var number = numberCell.text().trim();

    if (numberCell.hasClass("selected-number")) {
        numberCell.removeClass("selected-number");
        betAmount = null;
    }
    else {
        if (betAmount != null) {

            switch (betAmount) {
                case "0.5":
                    id = "05";
                    break;
                case "1.5":
                    id = "15";
                    break;
                case "2.5":
                    id = "25";
                    break;
                default:
                    id = betAmount;
                    break;
            }

            $('#bet-amount-' + id + '-cell').removeClass("selected-number");
        }
        numberCell.addClass("selected-number");
        betAmount = number;
    }
}

function SubmitBet() {
    var selectedNumbersConc = "";
    selectedNumbers.forEach(s => {
        selectedNumbersConc += s+"#"
    });

    if (selectedNumbersConc.length > 0) {
        selectedNumbersConc = selectedNumbersConc.slice(0, -1);
    }

    $('#new-bet-selected-numbers').val(selectedNumbersConc);
    $('#new-bet-number-of-selected-numbers').val(numberOfNumbers);
    $('#new-bet-number-of-draws').val(numberOfDraws);

    var amount = "";
    switch (betAmount) {
        case "0.5":
            amount = "0,5";
            break;
        case "1.5":
            amount = "1,5";
            break;
        case "2.5":
            amount = "2,5";
            break;
        default:
            amount = betAmount;
            break;
    }
    
    $('#new-bet-bet-amount').val(amount);

    selectedNumbers = [];
    numberOfNumbers = null;
    numberOfDraws = null;
    betAmount = null;


    $('#new-bet-form').submit();
}

$('#new-bet-page').ready(function () {
    var restoreNumbers = $('#RestoreNumbers');
    if (restoreNumbers !== null && restoreNumbers !== undefined && restoreNumbers.val() == "True") {
        restoreNewBetInput();
    }

    
});

function restoreNewBetInput() {
    for (var i = 1; i <= 12; i++) {

        var numberOfNumbersCell = $('#number-of-numbers-' + i + '-cell');
        if (numberOfNumbersCell != null && numberOfNumbersCell.hasClass("selected-number")) {
            numberOfNumbers = numberOfNumbersCell.text().trim();
            break;
        }
    }

    for (var i = 1; i <= 3; i++) {

        var numberOfDrawsCell = $('#number-of-draws-' + i + '-cell');
        if (numberOfDrawsCell != null && numberOfDrawsCell.hasClass("selected-number")) {
            numberOfDraws = numberOfDrawsCell.text().trim();
            break;
        }
    }

    for (var i = 1; i <= 9; i++) {

        var betAmountCell = $('#bet-amount-' + i + '-cell');
        if (betAmountCell != null && betAmountCell.hasClass("selected-number")) {
            betAmount = betAmountCell.text().trim();
            break;
        }
    }

    for (var i = 1; i <= 80; i++) {

        var numberCell = $('#number-' + i + '-cell');
        if (numberCell != null && numberCell.hasClass("selected-number")) {
            var number = numberCell.text().trim();
            selectedNumbers.push(number);
        }
    }

    console.log(selectedNumbers);
    console.log(numberOfNumbers);
    console.log(numberOfDraws);
    console.log(betAmount);
    
}