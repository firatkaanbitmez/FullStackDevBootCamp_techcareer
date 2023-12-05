// Film verileri
var filmler = [
  {
    id: 1,
    ad: "Film 1",
    yonetmen: "Yönetmen 1",
    aciklama: "Film 1 açıklaması.",
    resim: "https://via.placeholder.com/150",
  },
  {
    id: 2,
    ad: "Film 2",
    yonetmen: "Yönetmen 2",
    aciklama: "Film 2 açıklaması.",
    resim: "https://via.placeholder.com/150",
  },
  // Diğer film verileri...
];

// Film listesini oluşturma
var filmListesi = document.getElementById("filmListesi");

function filmListesiniGuncelle() {
  filmListesi.innerHTML = "";

  filmler.forEach(function (film, index) {
    var filmDiv = document.createElement("div");
    filmDiv.classList.add("film");
    filmDiv.dataset.id = film.id;

    var filmResim = document.createElement("img");
    filmResim.src = film.resim;
    filmDiv.appendChild(filmResim);

    filmDiv.addEventListener("click", function () {
      var filmDetay = document.getElementById("filmDetay");
      filmDetay.innerHTML = `<h2>${film.ad}</h2><p>Yönetmen: ${film.yonetmen}</p><p>${film.aciklama}</p>
        <button onclick="filmGuncelle(${film.id})">Güncelle</button>
        <button onclick="filmSil(${film.id})">Sil</button>`;
      filmDetay.classList.add("active");
    });

    filmListesi.appendChild(filmDiv);
  });
}

filmListesiniGuncelle();

// Yeni film ekleme formunu gösterme
function filmEkleFormuGoster() {
  var filmEkleFormu = document.getElementById("filmEkleFormu");
  filmEkleFormu.classList.toggle("active");
}

// Yeni film ekleme
function filmEkle() {
  var filmAd = document.getElementById("filmAd").value;
  var yonetmenAd = document.getElementById("yonetmenAd").value;
  var filmAciklama = document.getElementById("filmAciklama").value;
  var filmGorseli = document.getElementById("filmGorseli").value;

  var yeniFilm = {
    id: filmler.length + 1,
    ad: filmAd,
    yonetmen: yonetmenAd,
    aciklama: filmAciklama,
    resim: filmGorseli,
  };

  filmler.push(yeniFilm);
  filmEkleFormuGoster();
  filmListesiniGuncelle();
}

// Film güncelleme
function filmGuncelle(filmId) {
  var film = filmler.find((f) => f.id === filmId);
  if (!film) return;

  var yeniAd = prompt("Yeni film adı:", film.ad);
  var yeniYonetmen = prompt("Yeni yönetmen adı:", film.yonetmen);
  var yeniAciklama = prompt("Yeni film açıklaması:", film.aciklama);
  var yeniResim = prompt("Yeni film görsel URL'si:", film.resim);

  film.ad = yeniAd || film.ad;
  film.yonetmen = yeniYonetmen || film.yonetmen;
  film.aciklama = yeniAciklama || film.aciklama;
  film.resim = yeniResim || film.resim;

  filmListesiniGuncelle();
}

// Film silme
function filmSil(filmId) {
  var index = filmler.findIndex((f) => f.id === filmId);
  if (index === -1) return;

  filmler.splice(index, 1);
  filmListesiniGuncelle();
}
