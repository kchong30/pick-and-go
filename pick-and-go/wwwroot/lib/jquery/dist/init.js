/* Initial jQuery as Page Loaded */
(function ($) {
    $(document).ready(function () {
        var cart = JSON.parse(localStorage.getItem("cart"));
        $("#cart-icon").text(cart.length);
        if ($("#customize-page").html()) {
            var pp = $("#product option:selected").val();
            setProductPrice(pp);
        }
    });
})(jQuery);


/* Set Total Price from selected option value */
function setProductPrice(value) {
    // parse price from value
    var productValue = value.split("-");
    console.log(productValue)
    var productPrice = productValue[1];
    $("#product-price").text(productPrice);

    // display total price
    var totalPrice =
        parseFloat($("#product-price").html()) + parseFloat($("#ingredients-price").html());
    $("#total-price").text(totalPrice);
}