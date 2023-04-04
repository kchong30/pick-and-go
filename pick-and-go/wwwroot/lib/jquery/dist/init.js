/* Initial jQuery as Page Loaded */
(function ($) {
    $(document).ready(function () {
        var cart = JSON.parse(localStorage.getItem("cart"));
        if (cart != null) {
            $("#cart-icon").text(cart.length);
        }
        if ($("#customize-page").html()) {
            var pp = $("#product option:selected").val();
            setProductPrice(pp);
        }
    });

    $('.shopping-cart-btn').hover(function () {
        $(this).find('.cart-btn-info').toggleClass('btn-display')
    });
})(jQuery);


/* Set Total Price from selected option value */
function setProductPrice(value) {
    // parse price from value
    var productValue = value.split("-");
    var productPrice = productValue[1];
    $("#product-price").text(productPrice);

    // display total price
    var totalPrice =
        parseFloat($("#product-price").html()) + parseFloat($("#ingredients-price").html());
    $("#total-price").text(totalPrice.toFixed(2));
}


function checkout() {
    console.log("triggerd?")
    ajaxStoreCart();
}

function ajaxStoreCart() {
    var cart = localStorage.getItem("cart");
    $.ajax({
        type: "POST",
        url: "/Order/StoreCart",
        data: JSON.stringify({ CartJson: cart }),
        contentType: "application/json",
        success: function (response) {
            window.location.href
                = "/Order/ShoppingCart";
        },
        error: function (response) {
            console.log(response);
        }
    });
}
