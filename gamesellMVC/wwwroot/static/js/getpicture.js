$.ajax({

    url: '/Account/GetPicture',
    contentType: false,
    processData: false,
    type: 'GET',

    success: function (response) {
        $("#mainLogo").attr("src", response);
    },
    error: function (err) {
    }
})

$("#pic").click(function () {
    $.ajax({

        url: '/Account/GetPicture',
        contentType: false,
        processData: false,
        type: 'GET',

        success: function (response) {
            $("#bashi").attr("src", response);


        },
        error: function (err) {
        }

    })

    $.ajax({

        url: '/Account/GetBalance',
        contentType: false,
        processData: false,
        type: 'GET',

        success: function (response) {
            $("#balansId span").text(response);

        },
        error: function (err) {
        }
    })
})