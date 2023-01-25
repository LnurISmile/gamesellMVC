$("#ajax").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: '/Product/AutoComplete',
            data: { "q": request.term },
            type: "POST",
            success: function (data) {
                
                response($.map(data, function (item) {
                   /* alert(item.val); */
                    return item;
                    
                }))
            },
            error: function (response) {
                alert(response.responseText)
            },
            failure: function (response) {
                alert(response.responseText)
            }
        });
    },
    select: function (e, i) {
  /*      $("#hfPros").val(i.item.val); */
        window.location.href = "/Product/ProductDetails/" + i.item.val;
    },
    minLength: 1
});