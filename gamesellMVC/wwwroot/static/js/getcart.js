$("#cart").click(function () {
    var text = "";
    $.ajax({
        url: '/Account/GetCart',
        contentType: false,
        processData: false,
        type: 'GET',

        success: function (response) {
            $('#cartContentId').empty();
            if (response.length <= 0 || response.length == null) {
                text = "";
                text += '<div class="cart-content-empty">';
                text += '<p> </p>';
                text += '</div>';

                $('#cartContentId').append(text);
            }
            else {
                for (let i = 0; i < response.length; i++) {
                    text = "";
                    text += '<div class="cart-items" data-cart-item-id="' + response[i].id + '"><div class="cart-items-delete-overlay" data-deleteoverlay="' + response[i].id + '"><button class="cid-yes" onclick="deletecartitem(' + response[i].id + ')">@localizer["yes"]</button><button class="cid-no" onclick="hidedeletecartitem(' + response[i].id + ')">@localizer["no"]</button></div>';
                    text += '<div class="cart-image-content" style="background-image: url(/img/product/' + response[i].pImg + ');"> </div>';
                    text += '<div class="cart-info-content"> <h5 class="cic-title">' + response[i].pName + '</h5> <h3 class="cart-info-price">' + response[i].cur + ' <span>' + response[i].pPrice + '</span></h3> </div>';
                    text += '<div class="cart-control"> <button class="cart-item-delete" onclick="deleteCartItem(' + response[i].id + ')" id="btn_delete"> <i class="las la-trash"></i> </button> </div>';

                    $('#cartContentId').append(text);
                }
            }
        },
        error: function (err) {
        }
    })

    $.ajax({
        url: '/Account/GetTotal',
        contentType: false,
        processData: false,
        type: 'GET',

        success: function (response) {
            $('#total').text(response);
        },
        error: function (err) {
        }
    })

    $.ajax({
        url: '/Account/GetCur',
        contentType: false,
        processData: false,
        type: 'GET',

        success: function (response) {
            $('#curruncy').text(response);
        },
        error: function (err) {
        }
    })
})

function deleteCartItem(e) {
    $.ajax({
        url: '/Cart/DeleteFromCP?productId='+ e,
        contentType: false,
        processData: false,
        type: 'GET',
        
        success: function (response) {
            $("#cart").trigger('click');
            $("#cart").trigger('click');
        },
        error: function (err) {
        }
    })
}