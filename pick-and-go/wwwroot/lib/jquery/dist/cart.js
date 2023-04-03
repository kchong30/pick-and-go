/* Page Loaded */
(function ($) {
    $(document).ready(function () {
        $(".ingredientTable input:checked").each(function () {
            var clickedId = $(this).attr("id")
            var elementIdSplit = clickedId.split("-");
            var id = elementIdSplit[0];
            var quantityValue = elementIdSplit[1];
            updateTotalPrice(id, quantityValue)

        });
        checkoutBtnToggle();
    });
})(jQuery);


/* Checkout Button Toggle */
function checkoutBtnToggle() {
    var cart = JSON.parse(localStorage.getItem("cart"));
    if (cart == null) {
        $("#checkout-btn").hide();
    } else {
        if (cart.length < 1) {
            $("#checkout-btn").hide();
        } else {
            $("#checkout-btn").show();
        }
    }
}

/* Form Validation */
function validateForm() {
    // add required table here
    var validation = 0
    $('#Breads input:radio').each(function () {
        var value = $(this).val() // none
        var check = $(this).is(':checked') // true
        if (value == 'none' && check) {
            validation++;
        }
    })

    var length = $('#Breads form').length
    if (length == validation) {
        console.log("Select Bread!")
        return false;
    } else if ($("#product option:selected").val() == "") {
        console.log("Select Sandwich Size!")
        return false;
    } else {
        console.log("Validation okay!!")
        return true;
    }
}

/* Handle onChange Select Product Menu */
function changeProduct(event) {
    var value = event.target.value;
    setProductPrice(value);
}

/* Add to Cart */
function addToCart() {
    if (!validateForm()) {
        $(window).scrollTop(0);
        return false
    }

    var productPrice = $('#product-price').html();
    var productId = $("#product option:selected").val().split("-")[0];
    var description = $("#product option:selected").val().split("-")[2];
    var subTotal = 0;

    // Customized item array
    var ingredients = [];

    $(".ingredientTable").each(function () {
        var quantity = $(this).find(".quantity").html();
        if (quantity > 0) {
            var ingredient = {
                ingredientId: $(this).find(".ingredientId").html(),
                description: $(this).find(".description").html(),
                quantity: quantity,
                price: parseFloat($(this).find(".price").html()),
            };
            subTotal = quantity * ingredient.price + parseFloat(subTotal);
            ingredients.push(ingredient);
        }
    });

    var item = {
        productId: productId,
        productPrice: parseFloat(productPrice),
        description: description,
        ingredients: ingredients,
        subTotal: subTotal + parseFloat(productPrice)
    };

    // Get item from localStorage
    var cart = JSON.parse(localStorage.getItem("cart"));

    if (cart == null) {
        cart = [];
    }
    if (!(cart instanceof Array)) {
        cart = [cart];
    }

    cart.push(item);
    localStorage.setItem("cart", JSON.stringify(cart));
    $("#cart-icon").text(cart.length);
    clearSelection();
    checkoutBtnToggle();
    $('#add-message').text("Sandwich Added To Cart!").fadeIn(1000).delay(1000).fadeOut(1000);
}

/* Add to Cart from Fav Page */
function addToCartFromFav(event) {
    var data = event.target.dataset

    var productId = data.productid;
    var description = data.description;

    var productPrice = data.baseprice;
    var subTotal = 0;
    var favId = data.favid;
    var ingredients = [];
    var tableClassName = '.ingredientTable-' + favId

    $(tableClassName).each(function () {
        var quantity = $(this).find(".quantity").html();
        if (quantity > 0) {
            var ingredient = {
                ingredientId: $(this).find(".ingredientId").html(),
                description: $(this).find(".description").html(),
                quantity: quantity,
                price: $(this).find(".price").html(),
            };
            subTotal = quantity * ingredient.price + parseFloat(subTotal);
            ingredients.push(ingredient);
        }
    });

    var item = {
        productId: productId,
        productPrice: parseFloat(productPrice),
        description: description,
        ingredients: ingredients,
        subTotal: subTotal + parseFloat(productPrice)
    };

    // Get item from localStorage
    var cart = JSON.parse(localStorage.getItem("cart"));

    if (cart == null) {
        cart = [];
    }
    if (!(cart instanceof Array)) {
        cart = [cart];
    }

    cart.push(item);
    localStorage.setItem("cart", JSON.stringify(cart));
    $("#cart-icon").text(cart.length);
    //commented out clearSelection Call - KC - was changing quantity value in HTML to 0 after initial call.
    //clearSelection();

    $("#message").text("This sandwich has been added to your shopping cart!")
}

/* Add to Cart from Edit Page */
function addToCartFromEdit() {

    var productPrice;
    var productId;
    var description;
    var subTotal = 0.00;

    if (!validateForm()) {
        $(window).scrollTop(0);
        return false
    }

    productPrice = $('#product-price').html();
    productId = $("#product option:selected").val().split("-")[0];
    description = $("#product option:selected").val().split("-")[2];

    // Get item from localStorage
    var cart = JSON.parse(localStorage.getItem("cart"));

    if (cart == null) {
        cart = [];
    }
    if (!(cart instanceof Array)) {
        cart = [cart];
    }

    // Customized item array
    var ingredients = [];

    $(".ingredientTable").each(function () {
        var quantity = $(this).find(".quantity").html();
        if (quantity > 0) {
            var ingredient = {
                ingredientId: $(this).find(".ingredientId").html(),
                description: $(this).find(".description").html(),
                quantity: quantity,
                price: parseFloat($(this).find(".price").html()),
            };
            subTotal = quantity * ingredient.price + parseFloat(subTotal);
            ingredients.push(ingredient);
        }
    });

    var item = {
        productId: productId,
        productPrice: parseFloat(productPrice),
        description: description,
        ingredients: ingredients,
        subTotal: subTotal + parseFloat(productPrice)
    };

    cart.push(item);
    localStorage.setItem("cart", JSON.stringify(cart));

    clearSelection();
    checkoutBtnToggle()

    $("#message").text("This sandwich has been added to your shopping cart!")

    if (event.target.dataset.removeitem === "edit") {
        var index = event.target.dataset.index;
        removeSandwich(index);
    } else {
        ajaxStoreCart();
    }
}

/* Clear Selection */
function clearSelection() {
    console.log("Cleared!")
    // Uncheck radio button
    $("input:radio").each(function () {
        if ($(this).val() == "none") {
            $(this).prop("checked", true);
        } else {
            $(this).prop("checked", false);
        }
    });

    // Manually clear all sections
    var zero = 0.00;

    $(".quantity").each(function () {
        $(this).text(zero.toFixed(2));
    });

    $(".amount").each(function () {
        $(this).text(zero.toFixed(2));
    });

    $("#ingredients-price").text(zero.toFixed(2));

    $("#total-amt").text(zero.toFixed(2));

    var totalPrice = parseFloat($("#product-price").html());
    $("#total-price").text(totalPrice.toFixed(2));
}

/* Empty Cart */
function emptyCart() {
    $("#cart-icon").text(0);
    localStorage.clear();
}

/* Update Quantity from Selection Menu */
function changeQuantity(event) {
    var clickedId = event.target.id;
    var elementIdSplit = clickedId.split("-");
    var id = elementIdSplit[0];
    var quantityValue = elementIdSplit[1];

    updateTotalPrice(id, quantityValue);
}

function updateTotalPrice(id, quantityValue) {
    var itemPriceId = "#" + id + "-price";
    var cartQtyId = "#" + id + "-quantity";
    var cartAmtId = "#" + id + "-amount";

    var quantity = $(cartQtyId).html();

    switch (quantityValue) {
        case "none":
            quantity = 0;
            break;
        case "reg":
            quantity = 1;
            break;
        case "extra":
            quantity = 2;
            break;
    }

    $(cartQtyId).text(quantity);

    //Calculate new amount
    var amount = $(itemPriceId).html();
    var newAmount = (amount * quantity).toFixed(2);

    //console.log(newAmount)
    $(cartAmtId).text(newAmount);

    //Calculate totals
    var totalQuantity = 0;
    $(".quantity").each(function () {
        var thisQuantity = $(this).html();
        totalQuantity += parseInt(thisQuantity);
    });
    $("#total-amt").text(totalQuantity);

    var ingredientsPrice = 0;
    $(".amount").each(function () {
        var thisAmount = $(this).html();
        ingredientsPrice += parseFloat(thisAmount);
    });

    $("#ingredients-price").text(ingredientsPrice.toFixed(2));
    var totalPrice =
        parseFloat($("#product-price").html()) +
        parseFloat($("#ingredients-price").html());
    $("#total-price").text(totalPrice.toFixed(2));
}

/* Checkout */
function checkout(event) {
    ajaxStoreCart();
}

function removeSandwich(index) {

    var cart = JSON.parse(localStorage.getItem("cart"))
    //  var cart = localStorage.getItem("cart");

    cart.splice(index, 1);
    localStorage.setItem("cart", JSON.stringify(cart));

    $("#cart-icon").text(cart.length);
    ajaxStoreCart();
}

function ajaxStoreCart() {
    var cart = localStorage.getItem("cart");
    if (cart !== null) {
        if (cart.length > 0) {
            $.ajax({
                type: "POST",
                url: "/Order/StoreCart",
                data: JSON.stringify({ CartJson: cart }),
                contentType: "application/json",
                success: function (response) {
                    window.location.href = "/Order/ShoppingCart";
                },
                error: function (response) {
                    console.log(response);
                }
            });
            return
        }
    }
    window.location.href = "/Order/ShoppingCart";
}
