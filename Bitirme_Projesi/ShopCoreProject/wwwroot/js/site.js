// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {
  // Enable manual control with arrows
  $(".carousel-control-prev").click(function () {
    $("#productSlider").carousel("prev");
  });

  $(".carousel-control-next").click(function () {
    $("#productSlider").carousel("next");
  });

  // Automatically slide every 10 seconds
  function startSlider() {
    $("#productSlider").carousel({
      interval: 10000, // 10 seconds
      pause: "hover", // Pause on hover
      wrap: true, // Enable continuous loop
    });
  }

  // Start the slider
  startSlider();
});
