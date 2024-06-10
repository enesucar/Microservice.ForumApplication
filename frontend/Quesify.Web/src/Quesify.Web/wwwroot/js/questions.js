$("#next-btn").on("click", function () {
    var title = $('#create-question-form').find('input[name="Title"]').val();

    $.ajax({
        url: "/get-questions",
        type: "GET",
        data: {
            text: title
        }
    }).done(function (response) {
        $("#related-questions").removeClass("d-none");
        $('#next-btn').addClass("d-none");
        $('#create-question-btn').removeClass("d-none");
        $("#related-questions-table > tbody").empty();

        $.each(response.data.items, function (index, value) {
            var html =
                `<tr>
                    <td><a class="text-reset text-decoration-none" href="detail?id=${value.id}">${value.title}</a></td>
                    <td>${value.questionScore}</td>
                </tr>`;
            $("#related-questions-table > tbody").append(html);
        });

        if (response.data.items.length == 0) {
            var html =
                `<tr>
                    <td colspan="2">There are no related questions.</td>
                </tr>`;
            $("#related-questions-table > tbody").append(html);
        }
    });
});

$('#title').on('input', function () {
    $('#next-btn').removeClass("d-none");
    $('#create-question-btn').addClass("d-none");
    $("#related-questions").addClass("d-none");
});

