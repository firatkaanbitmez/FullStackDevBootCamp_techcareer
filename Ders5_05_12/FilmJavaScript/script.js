var filmler = [
  {
    id: 1,
    ad: "Film 1",
    yonetmen: "Yonetmen 1",
    aciklama: "Film 1 Hakkında Yazılanlar",
    resim: "https://via.placeholder.com/150",
  },
  {
    id: 2,
    ad: "Film 2",
    yonetmen: "Yonetmen 2",
    aciklama: "Film 2 Hakkında Yazılanlar",
    resim: "https://via.placeholder.com/150",
  },
];

var filmListesi = document.getElementById("filmListesi");
function filmListesiniGuncelle() {
  filmListesi.innerHTML = "";
  filmler.forEach(function (film, index) {
    var filmDiv = document.createElement("div");
    filmDiv.classList.add("film");

    var filmresim = document.createElement("img");
    filmresim.src = film.resim;
    filmDiv.appendChild(filmresim);

    filmDiv.addEventListener("click", function () {
      var filmDetay = document.getElementById("filmDetayi");
      filmDetay.innerHTML = `<h2>${film.ad}</h2><p>Yönetmen: ${film.yonetmen}</p><p>Açıklama: ${film.aciklama}</p>
    <button onclick="filmGuncelle(${film.id})">Güncelle</button>
    <button onclick="FilmSil(${film.id})">Sİl</button>`;
      filmDetay.classList.add("active");
    });
    filmListesi.appendChild(filmDiv);
  });
}
filmListesiniGuncelle();

function filmEkleFormuGoster() {
  var filmEkleFormu = document.getElementById("filmEkleFormu");
  filmEkleFormu.classList.toggle("active");
}
function filmEkle() {
  var filmAd = document.getElementById("filmAd").value;
  var yonetmenAd = document.getElementById("yonetmenAd").value;
  var filmaciklamasi = document.getElementById("filmaciklamasi").value;
  var filmGorseli = document.getElementById("filmGorseli").value;

  var yeniFilm = {
    id: filmler.length + 1,
    ad: filmAd,
    yonetmen: yonetmenAd,
    aciklama: filmaciklamasi,
    resim: filmGorseli,
  };
  filmler.push(yeniFilm);
  filmListesiniGuncelle();
  filmEkleFormuGoster();
}
function filmGuncelle(filmId) {
  var film = filmler.find((f) => f.id == filmId);
  if (!film) return;
  var yeniAd = prompt("Yeni Film Adı:", film.ad);
  var yeniYonetmen = prompt("Yeni yönetmen Adı:", film.yonetmen);
  var yeniAciklama = prompt("Yeni Film Açıklaması:", film.aciklama);
  var yeniResim = prompt("Yeni Film Resmi:", film.resim);

  film.ad = yeniAd || film.ad;
  film.yonetmen = yeniYonetmen || film.yonetmen;
  film.aciklama = yeniAciklama || film.aciklama;
  film.resim = yeniResim || film.resim;

  filmListesiniGuncelle();
}
function FilmSil(filmId) {
  var index = filmler.findIndex((f) => f.id == filmId);
  if (index == -1) return;
  filmler.splice(index, 1);
  filmListesiniGuncelle();
}
