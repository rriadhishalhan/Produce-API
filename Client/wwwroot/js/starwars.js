
$.ajax({
    url: "https://swapi.dev/api/people"
}).done((result) => {
    console.log(result.results);
    var text = "";
    $.each(result.results, function (key, val) {
        if (val.gender == "n/a") {
            var newGender = "-";
        }
        else {
            var newGender = val.gender;
        }
        text += `<tr>
                    <td>${key + 1}</td>
                    <td>${val.name}</td>
                    <td>${val.height}cm</td>
                    <td>${val.mass}kg</td>
                    <td>${newGender}</td>
                </tr>`;
    })
    console.log(text);
    $('#listStarwars').html(text);
}).fail((err) => {
    console.log(err);
})