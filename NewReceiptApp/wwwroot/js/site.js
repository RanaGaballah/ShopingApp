(function ($) {

    "use strict";


    // Add event listeners to all "Add to Cart" buttons
    document.querySelectorAll('.cart-button').forEach(button => {
        button.addEventListener('click', addToReceipt);
    });

    function addToReceipt(event) {
        // Prevent the default action of the button
        event.preventDefault();

        // Get the parent product-card element
        const productCard = event.target.closest('.product-card');

        // Extract item details
        const itemName = productCard.querySelector('.card-title a').innerText;
        const itemPrice = parseFloat(productCard.querySelector('.item-price').innerText.replace('$', ''));

        // Create a new row in the receipt table
        const receiptTable = document.querySelector('.invoice-items tbody');
        const newRow = document.createElement('tr');
        newRow.innerHTML = `
        <td>${itemName}</td>
        <td class="item-price alignright">$${itemPrice.toFixed(2)}</td>
    `;

        // Insert the new row before the total row
        const totalRow = receiptTable.querySelector('.total');
        receiptTable.insertBefore(newRow, totalRow);

        // Calculate the total amount
        let totalAmount = 0;
        receiptTable.querySelectorAll('tr:not(.total)').forEach(row => {
            const rowPrice = parseFloat(row.querySelector('.item-price').innerText.replace('$', ''));
            if (!isNaN(rowPrice)) {
                totalAmount += rowPrice;
            }
        });

        // Update the total price in the total row
        const totalAmountElement = totalRow.querySelector('.alignright');
        if (!isNaN(totalAmount)) {
            totalAmountElement.innerText = `$${totalAmount.toFixed(2)}`;
        }
    }

    document.addEventListener('DOMContentLoaded', function () {
        // Add event listener to the "Pay Now" button
        const payNowButton = document.querySelector('.btn-pay');
        payNowButton.addEventListener('click', processPayment);
    });

    // Define the processPayment function
    function processPayment() {
        // Get the paid amount entered by the user
        const paidAmountInput = document.getElementById('paid-amount');
        const paidAmount = parseFloat(paidAmountInput.value);

        // Get the total amount
        const totalAmountElement = document.getElementById('total-amount');
        const totalAmount = parseFloat(totalAmountElement.innerText.replace('$', ''));

        // Check if the paid amount is valid
        if (!isNaN(paidAmount) && paidAmount >= 0) {
            // Update the total amount display
            totalAmountElement.innerText = `$${totalAmount.toFixed(2)}`;

            // Check if the paid amount is sufficient
            if (paidAmount >= totalAmount) {
                // Payment successful
                alert('Payment successful!');
            } else {
                // Payment insufficient
                const remainingAmount = totalAmount - paidAmount;
                alert(`Insufficient payment. Remaining amount: $${remainingAmount.toFixed(2)}`);
            }
        } else {
            // Invalid paid amount
            alert('Invalid paid amount.');
        }
    }















    var searchPopup = function () {
        // open search box
        $('#header-nav').on('click', '.search-button', function (e) {
            $('.search-popup').toggleClass('is-visible');
        });

        $('#header-nav').on('click', '.btn-close-search', function (e) {
            $('.search-popup').toggleClass('is-visible');
        });

        $(".search-popup-trigger").on("click", function (b) {
            b.preventDefault();
            $(".search-popup").addClass("is-visible"),
                setTimeout(function () {
                    $(".search-popup").find("#search-popup").focus()
                }, 350)
        }),
            $(".search-popup").on("click", function (b) {
                ($(b.target).is(".search-popup-close") || $(b.target).is(".search-popup-close svg") || $(b.target).is(".search-popup-close path") || $(b.target).is(".search-popup")) && (b.preventDefault(),
                    $(this).removeClass("is-visible"))
            }),
            $(document).keyup(function (b) {
                "27" === b.which && $(".search-popup").removeClass("is-visible")
            })
    }

    var initProductQty = function () {

        $('.product-qty').each(function () {

            var $el_product = $(this);
            var quantity = 0;

            $el_product.find('.quantity-right-plus').click(function (e) {
                e.preventDefault();
                var quantity = parseInt($el_product.find('#quantity').val());
                $el_product.find('#quantity').val(quantity + 1);
            });

            $el_product.find('.quantity-left-minus').click(function (e) {
                e.preventDefault();
                var quantity = parseInt($el_product.find('#quantity').val());
                if (quantity > 0) {
                    $el_product.find('#quantity').val(quantity - 1);
                }
            });

        });

    }

    $(document).ready(function () {

        searchPopup();
        initProductQty();

        var swiper = new Swiper(".main-swiper", {
            speed: 500,
            navigation: {
                nextEl: ".swiper-arrow-prev",
                prevEl: ".swiper-arrow-next",
            },
        });

        var swiper = new Swiper(".product-swiper", {
            slidesPerView: 4,
            spaceBetween: 10,
            pagination: {
                el: "#mobile-products .swiper-pagination",
                clickable: true,
            },
            breakpoints: {
                0: {
                    slidesPerView: 2,
                    spaceBetween: 20,
                },
                980: {
                    slidesPerView: 4,
                    spaceBetween: 20,
                }
            },
        });

        var swiper = new Swiper(".product-watch-swiper", {
            slidesPerView: 4,
            spaceBetween: 10,
            pagination: {
                el: "#smart-watches .swiper-pagination",
                clickable: true,
            },
            breakpoints: {
                0: {
                    slidesPerView: 2,
                    spaceBetween: 20,
                },
                980: {
                    slidesPerView: 4,
                    spaceBetween: 20,
                }
            },
        });

        var swiper = new Swiper(".testimonial-swiper", {
            loop: true,
            navigation: {
                nextEl: ".swiper-arrow-prev",
                prevEl: ".swiper-arrow-next",
            },
        });

    }); // End of a document ready

})(jQuery);