$(document).ready(function () {
  var main = function () {
    $(".fa-bars").click(function () {
      $(".nav-screen").animate(
        {
          right: "0px",
        },
        200
      );

      $("body").animate(
        {
          right: "285px",
        },
        200
      );
    });

    $(".fa-times").click(function () {
      $(".nav-screen").animate(
        {
          right: "-285px",
        },
        200
      );

      $("body").animate(
        {
          right: "0px",
        },
        200
      );
    });

    $(".nav-links a").click(function () {
      $(".nav-screen").animate(
        {
          right: "-285px",
        },
        500
      );

      $("body").animate(
        {
          right: "0px",
        },
        500
      );
    });
  };

  $(document).ready(main);
});
var swiper = new Swiper(".swiper-container", {
  effect: "coverflow",
  grabCursor: true,
  centeredSlides: true,
  navigation: true,
  coverflowEffect: {
    rotate: 0,
    stretch: 0,
    depth: 0,
    modifier: 0,
    slideShadows: false,
  },
  keyboard: {
    enabled: true,
  },
  mousewheel: {
    thresholdDelta: 70,
  },
  loop: true,
  loopAdditionalSlides: 1,
  autoplay: {
    delay: 10000, //5 seconds delay
  },
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
  breakpoints: {
    760: {
      slidesPerView: 1,
      spaceBetween: 0,
    },
    888: {
      slidesPerView: 2,
      spaceBetween: 30,
      centeredSlides: false,
    },
    1366: {
      slidesPerView: 3,
      spaceBetween: 30,
    },
    1920: {
      slidesPerView: 3,
      spaceBetween: 30,
    },
  },
});

function showNotification(message) {
  const notification = document.getElementById("notification");
  notification.innerHTML = message;
  notification.style.display = "block";
  notification.style.fontSize = "25px";
  notification.style.font = "large";

  // 3 saniye sonra bildirimi gizle
  setTimeout(() => {
    notification.style.display = "none";
  }, 2000);
}

document.querySelectorAll(".social-icon").forEach((icon) => {
  icon.addEventListener("click", (e) => {
    e.preventDefault();
    let site = "";
    if (icon.classList.contains("social-icon--envelope")) {
      site = "firatbitmez@outlook.com";
      // Copy email address to clipboard
      const textArea = document.createElement("textarea");
      textArea.value = site;
      document.body.appendChild(textArea);
      textArea.select();
      document.execCommand("copy");
      document.body.removeChild(textArea);

      showNotification("Email address copied.");

      return;
    } else if (icon.classList.contains("social-icon--codepen")) {
      site = "https://codepen.io/FIRAT-KAAN-BTMEZ";
    } else if (icon.classList.contains("social-icon--github")) {
      site = "https://github.com/firatkaanbitmez";
    } else if (icon.classList.contains("social-icon--twitter")) {
      site = "https://twitter.com/";
    } else if (icon.classList.contains("social-icon--dribbble")) {
      site = "https://dribbble.com/";
    } else if (icon.classList.contains("social-icon--instagram")) {
      site = "https://www.instagram.com/firatbitmez/";
    } else if (icon.classList.contains("social-icon--linkedin")) {
      site = "https://www.linkedin.com/in/firatkaanbitmez/";
    } else if (icon.classList.contains("social-icon--facebook")) {
      site = "https://www.facebook.com/";
    } else if (icon.classList.contains("social-icon--Hackerrank")) {
      site = "https://www.hackerrank.com/profile/firatbitmez";
    } else if (icon.classList.contains("social-icon--youtube")) {
      site = "https://www.youtube.com/@firatkaanbitmez";
    }

    if (site) {
      window.open(site, "_blank");
    }
  });
});

document.addEventListener("DOMContentLoaded", function () {
  var imageContainer = document.querySelector(".image-container");
  var editImageContainer = document.querySelector(".editimage-container");
  var readMoreBtn = document.getElementById("readMoreBtn");

  // Set the initial state
  imageContainer.style.display = "flex";
  editImageContainer.style.display = "none";

  readMoreBtn.addEventListener("click", function () {
    if (
      imageContainer.style.display === "none" ||
      imageContainer.style.display === ""
    ) {
      imageContainer.style.display = "flex";
      editImageContainer.style.display = "none";
      readMoreBtn.innerText = "Show More";
    } else {
      imageContainer.style.display = "none";
      editImageContainer.style.display = "flex";
      readMoreBtn.innerText = "Show Less";
    }
  });
});

document
  .querySelector(".custom-text-container")
  .addEventListener("click", function () {
    window.location.href = "https://firatbitmez.com";
  });

document
  .querySelector("#menu-textcontainer")
  .addEventListener("click", function () {
    window.location.href = "https://firatbitmez.com";
  });

document.addEventListener("DOMContentLoaded", function () {
  var backgroundContainer = document.getElementById("background-container");
  var images = backgroundContainer.getElementsByTagName("img");

  // Rastgele bir sayı üret ve bu sayıya karşılık gelen resmi seç
  var randomIndex = Math.floor(Math.random() * images.length);
  var selectedImage = images[randomIndex];
  selectedImage.style.opacity = "1";

  // Scroll olayını dinle
  var menuContainer = document.getElementById("menu-container");
  var mobilBar = document.querySelector(".mobil-bar");
  var lastScrollTop = 0;

  window.addEventListener("scroll", function () {
    var scrollTop = window.scrollY || document.documentElement.scrollTop;
    var scrollDirection = scrollTop > lastScrollTop ? "down" : "up";

    // Sayfa en üstte olduğunda
    if (scrollTop === 0) {
      // Resmi görünür hale getir
      selectedImage.style.opacity = "1";
      menuContainer.style.backgroundColor = "transparent";
      mobilBar.style.backgroundColor = "transparent";
    } else {
      // Resmi gizle
      selectedImage.style.opacity = "0";
    }

    // Sayfa aşağı kaydırıldığında
    if (scrollDirection === "down") {
      // Arka plan rengini değiştir ve resmi yavaşça yukarı kaydır
      menuContainer.style.backgroundColor = "#0a0a0a";
      mobilBar.style.backgroundColor = "#0a0a0a";
      backgroundContainer.style.transform = "translateY(-100%)";
    } else {
      // Sayfa en üste gelince şeffaf yap ve resmi yavaşça aşağı kaydır

      backgroundContainer.style.transform = "translateY(0)";
      if (scrollDirection === "up") {
        backgroundContainer.style.height = "50vh";
      }
    }

    // Scroll pozisyonunu güncelle
    lastScrollTop = scrollTop;
  });
  // Sayfa enüstte değilse ve sayfa refreshlenirse selectedimage opacity 0 olmalı
  window.addEventListener("load", function () {
    var scrollTop = window.scrollY || document.documentElement.scrollTop;
    if (scrollTop !== 0) {
      selectedImage.style.opacity = "0";
      menuContainer.style.backgroundColor = "#0a0a0a";
      mobilBar.style.backgroundColor = "#0a0a0a";
      backgroundContainer.style.transform = "translateY(-100%)";
    }
  });
});

document.addEventListener("DOMContentLoaded", function () {
  var links = document.querySelectorAll(".nav-links a, #menu a");

  links.forEach(function (link) {
    link.addEventListener("click", function (e) {
      e.preventDefault();

      var targetId = link.getAttribute("href").substring(1);
      console.log("Target ID:", targetId);

      if (targetId === "blog") {
        window.location.href = "http://blog.firatbitmez.com";
      }
      if (targetId === "home") {
        window.location.href = "http://firatbitmez.com";
      } else {
        var targetSection = document.querySelector("." + targetId);
        console.log("Target Section:", targetSection);

        if (targetSection) {
          var offset = 100; // İstenen mesafe
          var targetPosition =
            targetSection.getBoundingClientRect().top + window.scrollY;
          window.scroll({
            top: targetPosition - offset,
            behavior: "smooth",
          });
        } else {
          console.error("Target section not found!");
        }
      }
    });
  });
});
