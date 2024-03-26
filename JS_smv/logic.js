let velikost;
let izzrebaneStevilke = [];
let izbranaTabela = null;
let interval;
function omogociIgro() {
    velikost = Number(document.getElementById("steviloKartic").value);
    if (velikost == 0) {
        document.getElementById("novaIgra").disabled = true;
        document.getElementById("naprej").disabled = true;
        document.getElementById("bingo").disabled = true;
    } else {
        document.getElementById("novaIgra").disabled = false;
        document.getElementById("naprej").disabled = false;
        document.getElementById("bingo").disabled = false;
        racunalnikIzbere();
    }
}
function pozeniIgro() {
    let stVrstic = Number(document.getElementById("steviloKartic").value);
    let podatkiDiv = document.getElementById("podatki");
    pozeniTimer();
    zvok('start.wav');
    podatkiDiv.innerHTML = '';
    izzrebaneStevilke = [];
    if (stVrstic === 3) {
        let tabela = '';
        for (let i = 0; i < 5; i++) {
            tabela += '<table>';
            for (let j = 0; j < 3; j++) {
                tabela += '<tr>';
                for (let stolp = 0;stolp < 5;stolp++) {
                    let st = i * 15 + j * 5 +stolp + 1;
                    if (st <= 75) {
                        tabela += '<td>' + st + '</td>';
                    }
                }
                tabela += '</tr>';
            }
            tabela += '</table>';
            tabela += '<br/>'
        }
        podatkiDiv.innerHTML += tabela;
    } else if (stVrstic === 4) {
        let tabela = '';
        for (let i = 0; i < 5; i++) {
            tabela += '<table>';
            for (let j = 0; j < 4; j++) {
                tabela += '<tr>';
                for (let k = 0; k < 5; k++) {
                    let st = i * 20 + j * 5 + k + 1;
                    if (st <= 80) {
                        tabela += '<td>' + st + '</td>';
                    }
                }
                tabela += '</tr>';
            }
            tabela += '</table>';
            tabela += '<br/>'
        }
        podatkiDiv.innerHTML += tabela;
    } else if (stVrstic === 5) {
        let tabela = '';
        for (let i = 0; i < 3; i++) {
            tabela += '<table>';
            for (let j = 0; j < 5; j++) {
                tabela += '<tr>';
                for (let k = 0; k < 5; k++) {
                    let st = i * 25 + j * 5 + k +1;
                    if (st <= 75) {
                        tabela += '<td>' + st + '</td>';
                    }
                }
                tabela += '</tr>';
            }
            tabela += '</table>';
            tabela += '<br/>'
        }
        podatkiDiv.innerHTML += tabela;
    }
    racunalnikIzbere();
    racunalnikKopira();
    dodajanje();
}

function kopirajTabelo(tab) {
    let clone = tab.cloneNode(true);
    let igralecDiv = document.getElementById("igralec");
    let ime = igralecDiv.querySelector("h2").cloneNode(true);
    let button = igralecDiv.querySelectorAll("button");
    igralecDiv.innerHTML='';
    igralecDiv.appendChild(ime);
    document.getElementById("igralec").appendChild(clone);

    button.forEach(function (button){
        igralecDiv.appendChild(button.cloneNode(true)) ;
    });

}

function dodajanje() {
    let tAbela = document.querySelectorAll("#podatki table");
    tAbela.forEach(function (tab) {
        tab.addEventListener('click', function () {
            if(tab !== izbranaTabela)
            {
                kopirajTabelo(tab);
            }
        });
    });
}
function  racunalnikIzbere()
{
    let taBele = document.querySelectorAll("#podatki table");
    let random = Math.floor(Math.random()* taBele.length);
    izbranaTabela = taBele[random];
    racunalnikKopira(izbranaTabela);
}
function racunalnikKopira()
{
    if (izbranaTabela)
    {
        let kloniri = izbranaTabela.cloneNode(true);
        let racunalnikDiv =  document.getElementById("racunalnik");
        let ime = racunalnikDiv.querySelector("h2").cloneNode(true);
        racunalnikDiv.innerHTML = '';
        racunalnikDiv.appendChild(ime);
        document.getElementById("racunalnik").appendChild(kloniri);

    }
}
function  genereiraj() {
    let stevila = document.querySelectorAll("#podatki table td:not(.izbran)");
    if (stevila.length === 0) {
        alert("Vse številke so izžrebane");
        ustaviTimer();
        return;
    }
    stevila.forEach(function(stevilke) {
        stevilke.style.color = "";
    });
    let randomIndex = Math.floor(Math.random() * stevila.length);
    let celica = stevila[randomIndex];
    let izbranaStevilka = celica.textContent;

    celica.classList.add("izbran");
    celica.style.backgroundColor = "green";

    document.getElementById("novoStevilo").textContent = izbranaStevilka;
    izzrebaneStevilke.push(parseInt(izbranaStevilka));
    let racunalnikCelice = document.querySelectorAll("#racunalnik table td");
    racunalnikCelice.forEach(function (cell)  {
        if (cell.textContent === izbranaStevilka && !cell.classList.contains("izbran")) {
            cell.style.backgroundColor = "green";
            cell.classList.add("izbran");
        }

        });
    racunalnikPreveriZmago();
}

function oznaciCelico() {
   let igralec = document.querySelectorAll("#igralec table td");
    igralec.forEach(function(celica) {
        celica.addEventListener('click', function (){
            if(izzrebaneStevilke.includes(parseInt(this.textContent))){
                this.style.cursor = "pointer";
                this.style.backgroundColor = 'yellow';
            }
        });
    });
}

function preveriZmago() {
    let igralecZmaga = preveriPetZaporednih("igralec", "yellow");
    if (igralecZmaga) {
        let oznaceneCelice = document.querySelectorAll("#igralec table td.yellow");
        let izzreban = true;
        oznaceneCelice.forEach(function(cell) {
            let number = parseInt(cell.textContent);
            if (!izzrebaneStevilke.includes(number)) {
               izzreban = false;

            }
        });
        if (izzreban) {
            zvok('bingo.wav');
            alert("Čestitke, zmagali ste!");

        }
    }
   ustaviTimer();
}
function racunalnikPreveriZmago()
{
    let racunalnikZ = preveriPetZaporednih("racunalnik", "green");
    if (racunalnikZ) {
        zvok('lose.wav');
        alert("Računalnik je zmagal");
    }
}
function preveriPetZaporednih(tableId, color) {
    let table = document.getElementById(tableId);
    let vrstice = table.querySelectorAll("tr");

    for (let i = 0; i < vrstice.length; i++) {
        let cells = vrstice[i].querySelectorAll("td");
        let stej = 0;
        for (let j = 0; j < cells.length; j++) {
            if (cells[j].style.backgroundColor === color) {
                stej++;
                if (stej === 5) {
                    return true;
                }
            } else {
                stej = 0;
            }
        }
    }
    return false;
}
function  zvok(url)
{
   let audio = document.getElementById("zvok");
   audio.src = url;
   audio.play();
}
function pozeniTimer(){
    interval = setInterval(function(){izpisCasa();}, 1000);
    let klik_aktiven=1;
}
let klik_aktiven=0;
function ustaviTimer(){
    clearInterval(interval);
    let klik_aktiven=0;
}
let cas = 0;
function izpisCasa(){
    cas++;
    let sec = cas % 60;
    let  min = (cas - sec)/60;
    min += ':'
    if(sec < 10)min+='0';
    min += sec;
    document.getElementById('casIgranja').innerHTML = min;
}
