$(document).ready(function () {

});

$("#gnId").change(function () {

    $.ajax({

        url: '/Product/GetItem',

        async: false,
        type: 'GET',

        data: 'ID=' + $('#gnId').val(),
        success: function (data) {

            tableText = data;

            $("#giId option").remove();
            $("#giId").append(data);
        },
    })
})