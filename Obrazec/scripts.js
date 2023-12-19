var maxPrijavljenih;
var stPrijavljenih = [];
var oddelki = [];
var skupine = [
  "B. Resinovič", "Humanoidni robot NAO in Brain Computer Interface", "Vsebine: programiranje robota Nao, izdelava programa z uporabo možgansko računalniškega vmesnika",
  "B. Lubej", "Python", "Vsebine: osnovni ukazi in metode za delo, izdelava preproste igre",
  "J. Koren", "Processing", "Vsebine: programiranje proceduralnih animacij in generiranje slik, izdelava interaktivne animacije",
  "B. Slemenšek", "JavaScript", "JavaScript, JS za postavitev aktivnih strani, izdelava igre"
];
var stSkupin;
var EMSO_ok = 0;

function Connect() {
  prejetOdgovor();
}

function prejetOdgovor() {
  maxPrijavljenih = 15;
  stSkupin = 4;

  stPrijavljenih[0] = 3;
  stPrijavljenih[1] = 5;
  stPrijavljenih[2] = 12;
  stPrijavljenih[3] = 1;

  oddelki[0] = "R4A";
  oddelki[1] = "R4A";

  izpisPodatkov();
  izpisObrazca();
}

function izpisPodatkov() {
  var str = 'Skupina	Prijavljeni	Nosilec	Tematika\n';

  for (var i = 0; i < stSkupin; i++) {
    str += i + 1 + '	' + stPrijavljenih[i] + '	' + skupine[i * 3] + '	' + skupine[i * 3 + 1] + ' ' + skupine[i * 3 + 2] + '\n';
  }

  console.log(str);
}

function izpisObrazca() {
  var str = 'Največje število prijavljenih v skupini: ' + maxPrijavljenih + '\n';
  str += 'Za prijavo v skupino moraš vnesti naslednje podatke:\nEMŠO: <input type="text" id="EMSOdijaka" onkeyup="preveriStevke()">\nOddelek: <select id="Oddelekdijaka">\n';

  for (var i = 0; i < oddelki.length; i++) {
    str += '<option value="' + oddelki[i] + '">' + oddelki[i] + '</option>\n';
  }

  str += '</select>\nPrva izbira: <select id="prvaizbira">\n';
  
  for (var i = 1; i < skupine.length; i += 3) {
    str += '<option value="' + i + '">' + skupine[i] + '</option>\n';
  }

  str += '</select>\nDruga izbira: <select id="drugaizbira" disabled="true" onclick="PosljiPodatke()"></select>\n';

  console.log(str);
}

function preveriStevke() {
  var pod = document.getElementById("EMSOdijaka").value;
  var text = '';

  for (var i = 0; i < pod.length; i++) {
    var ch = pod.charAt(i);
    if ((ch >= '0') && (ch <= '9')) text += ch;
    else break;
  }

  document.getElementById("EMSOdijaka").value = text;

  if (text.length == 13) preveriEmso();
}

function preveriEmso() {
  EMSO_ok = 0;
  var emso = document.getElementById("EMSOdijaka").value;

  if (emso.length == 13) {
    var sum = 0;
    var num;
    var pond = 7;

    for (var poz = 0; poz < 12; poz++) {
      num = Number(emso.charAt(poz));

      if (pond == 1) pond = 7;
      num *= pond--;

      sum += num;
    }

    sum % 11;

    if (sum < 2) sum = 0;
    else sum = 11 - sum;

    num = Number(emso.charAt(12));

    if (sum == num) EMSO_ok = 1;
  }

  if ((EMSO_ok === 0) && (emso !== '')) alert('NAPAKA v EMŠO');
  omogociPoslji();
}

function omogociPoslji() {
  var omogoci = true;

  if (EMSO_ok === 1) {
    if (document.getElementById("Oddelekdijaka").value !== "0") {
      if (document.getElementById("prvaizbira").value !== "-1") {
        if (document.getElementById("drugaizbira").value !== "-1") {
          omogoci = false;
        }
      }
    }
  }

  document.getElementById("gumbposlji").disabled = omogoci;
}

function pripraviDrugoIzbiro() {
  var str = '';
  var prva = document.getElementById("prvaizbira").value;

  if (prva !== "-1") {
    str += '<option value="-1">--izberi skupino--</option>\n';

    for (var i = 1; i < skupine.length; i += 3) {
      str += '<option value="' + i + '">' + skupine[i] + '</option>\n';
    }
  } else {
    str += '<option value="-1">Najprej izberi prvo možnost!</option>\n';
  }

  document.getElementById("drugaizbira").innerHTML = str;
}

function PosljiPodatke() {
  var str = 'EMŠO: ' + document.getElementById('EMSOdijaka').value;
  str += '\nOddelek: ' + document.getElementById('Oddelekdijaka').value;
  str += '\nPrva Izbira: ' + document.getElementById('prvaizbira').value;
  str += '\nDruga Izbira: ' + document.getElementById('drugaizbira').value;

  alert(str);
}
