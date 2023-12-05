//var sayi=5;  //var kelime ile karşılığına gelen değeri alır.
//var name="John"; //yani var ile bir değişkenin string mi int mı olduğunu tanımlamamıza gerek yok.

//let kavramını local olarak düşünüp kullanabiliriz
//const ise sabit bir dğeişken oluşturmak için kullanılır. Bir kez değer atandığında bu değer değiştirilemez.
//const pi=3.14;

//değişken isimlerinde harf,sayı vs unicode gibi elemanları kullanabiliriz.
/*
var meyveler = ["elma", "armut", "muz", "çilek"]; // 4elemanlı bir dizi oluşturduk
//min index 0  max index 3
selamver("ahmet");
function selamver(isim) {
  console.log("Merhaba" + isim + "!");
}

var sayi = 5;

if (sayi > 3) {
  console.log("Sayi 3 den büyüktür");
} else {
  console.log("Girilen sayi 3'ten küçük veya eşittir");
}

for (var i = 0; i < meyveler.length; i++) {
  console.log(meyveler[i]);
}
*/
function Ekle() {
  var alisverisEkle = document.getElementById("alisverisEkle"); //
  var liste = document.getElementById("liste");
  var yeniOge = document.createElement("li");
  yeniOge.innerText = alisverisEkle.value;

  if (alisverisEkle.value !== "") {
    //boş değilse
    liste.appendChild(yeniOge);
    alisverisEkle.value = "";

    //güncelleme
    yeniOge.onclick = function () {
      var yenimetin = prompt("Yeni değeri giriniz");
      if (yenimetin !== null && yenimetin !== "") {
        this.firstChild.nodeValue = yenimetin;
      }
    };

    //tekli silme
    yeniOge.addEventListener("contextmenu", function (e) {
      e.preventDefault();
      this.paretNode.removeChild(yeniOge);
    });
  } else {
    alert("lütfen bir öğe giriniz");
  }
}

//Tüm listeyi silme
function listeTemizle() {
  var liste = document.getElementById("liste");
  liste.innerHTML = "";
}
