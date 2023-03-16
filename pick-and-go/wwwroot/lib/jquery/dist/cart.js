/* Page Loaded, setProducePrice has been moved to init.js due to cart icon */


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


/* Add to Cart from Favorite */
/*
 1) Which table is selected?
 2) What do the table have ? (productId, ingredients, subtotal)
 3) Parse to float and send it to shoopping cart
 4) Remove fav part from 'add to cart btn'
  */


/* Add to Cart */
function addToCart(event) {
    var subTotal;
    var productId;
    var description;

    if ($("#customize-page").html()) {
        if (!validateForm()) {
            $(window).scrollTop(0);
            return false
        }
        subTotal = $('#product-price').html();
        productId = $("#product option:selected").val().split("-")[0];
        description = $("#product option:selected").val().split("-")[2];
    } else {
        console.log("Fav Page")
        if (event) {
            subTotal = event.target.value.split("-")[2];
            productId = event.target.value.split("-")[0];
            description = event.target.value.split("-")[1];
            /* new values were not changed.. so left comment here */
            /* Reason of error: event parameter was missing, so temporary added paramter in Favorites.cshtml */
        }
    }

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
        description: description,
        ingredients: ingredients,
        subTotal: subTotal
    };

    cart.push(item);
    localStorage.setItem("cart", JSON.stringify(cart));
    $("#cart-icon").text(cart.length);
    clearSelection();

    $("#message").text("This sandwich has been added to your shopping cart!")
}

/* Add to Cart from Edit Page */
function addToCartFromEdit() {


    var subTotal;
    var productId;
    var description;


        if (!validateForm()) {
            $(window).scrollTop(0);
            return false
        }

        subTotal = $('#product-price').html();
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
        description: description,
        ingredients: ingredients,
        subTotal: subTotal
    };

    cart.push(item);
    localStorage.setItem("cart", JSON.stringify(cart));

    clearSelection();

    $("#message").text("This sandwich has been added to your shopping cart!")

    if (event.target.dataset.removeitem === "edit") {
        var index = event.target.dataset.index;
        removeSandwich(index);
    }else{
        ajaxStoreCart();
    }
}

/* Clear Selection */
function clearSelection() {
    // Uncheck radio button
    $("input:radio").each(function () {
        if ($(this).val() == "none") {
            $(this).prop("checked", true);
        } else {
            $(this).prop("checked", false);
        }
    });

    // Manually clear all sections

    $(".quantity").each(function () {
        $(this).text(0);
    });

    $(".amount").each(function () {
        $(this).text(0);
    });

    $("#ingredients-price").text(0);

    $("#total-amt").text(0);

    var totalPrice = parseFloat($("#product-price").html());
    $("#total-price").text(totalPrice);
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

    console.log(newAmount)
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
    $("#total-price").text(totalPrice);
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

    if (cart.length === 0) {
        // display alert with message
        alert("Your shopping cart is empty, please click the button to proceed");
        // navigate to shopping cart page
        window.location.href = "/Order";
        return;
    }
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
