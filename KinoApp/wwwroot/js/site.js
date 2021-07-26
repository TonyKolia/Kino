var selectedNumbers = [];

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