$(document).ready(function () {

});

$("#btnpay").click(function () {

    $.ajax({

        url: '/Account/EditBalanceInfo',

        async: false,
        type: 'GET',

        data: 'ID=' + $('#balinfoId').val(),
        success: function (data) {

        },
    })
})