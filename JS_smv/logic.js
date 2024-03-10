let velikost;
let stevilke = [];
let izzrebaneStevilke = [];
let izbranaTabela = null;
function omogociIgro() {
    velikost = Number(document.getElementById("steviloKartic").value);
    if (velikost == 0) {
        document.getElementById("novaIgra").disabled = true;
        document.getElementById("naprej").disabled = true;
        //document.getElementById("igralnoPolje").innerHTML='';
    } else {
        document.getElementById("novaIgra").disabled = false;
        document.getElementById("naprej").disabled = false;
        racunalnikIzbere();
    }
}

function pozeniIgro() {
    let stVrstic = Number(document.getElementById("steviloKartic").value);
    let podatkiDiv = document.getElementById("podatki");
    pozeniTimer();
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
                        stevilke.push(st);
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
                        stevilke.push(st);
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
                        stevilke.push(st);
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
    zvok('audio.mp3');
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

let interval;
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

function  genereiraj() {
    let stevila = document.querySelectorAll("#podatki table td:not(.izbran)");
    if (stevila.length === 0) {
        alert("Vsa številke so izžrebane");
        ustaviTimer();
        return;
    }
    stevila.forEach(numberCell => {
        numberCell.style.color = ""; // Remove the color style
    });
    let randomIndex = Math.floor(Math.random() * stevila.length);
    let celica = stevila[randomIndex];
    let izbranaStevilka = celica.textContent;

    //Beleži izbrane številke
    celica.classList.add("izbran");
    celica.style.backgroundColor = "green";
    // Prikaže izžrebano številko
    document.getElementById("novoStevilo").textContent = izbranaStevilka;
    izzrebaneStevilke.push(parseInt(izbranaStevilka));
    let racunalnikCelice = document.querySelectorAll("#racunalnik table td");
    let consecutiveCount = 0;
    racunalnikCelice.forEach(cell => {
        if (cell.textContent === izbranaStevilka && !cell.classList.contains("izbran")) {
            cell.style.backgroundColor = "green"; // Change background color if number matches
            cell.classList.add("izbran");
            consecutiveCount++;
            if(consecutiveCount ===5)
            {
               let racunalnikZ=preveriPetZaporednih("racunalnik","green");
                if(racunalnikZ)
                {
                    alert("Računalnik je zmagal");
                }
            }
        }
        else {
            consecutiveCount =0;
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
    let racunalnikZmaga = preveriPetZaporednih("racunalnik", "green");
    if (igralecZmaga) {
        let oznaceneCelice = document.querySelectorAll("#igralec table td.yellow");
        let allNumbersPicked = true;
        oznaceneCelice.forEach(function(cell) {
            let number = parseInt(cell.textContent);
            if (!izzrebaneStevilke.includes(number)) {
                allNumbersPicked = false;

            }
        });
        if (allNumbersPicked) {
            zvok('bingo.wav');
            alert("Čestitke, zmagali ste!");

        }

    }
    if (racunalnikZmaga) {
        let oznaceneCElice = document.querySelectorAll("#racunalnik table td.green");
        let racunalnikStevilke = [];

        oznaceneCElice.forEach(function(cell) {
            let number = parseInt(cell.textContent);
            racunalnikStevilke.push(number);
        });

        let allNumbersDrawn = izzrebaneStevilke.every(function(number) {
            return racunalnikStevilke.includes(number);
        });

        if (allNumbersDrawn) {
            alert("Računalnik je zmagal");
            ustaviTimer();
        }
    }



   ustaviTimer();
}
function racunalnikPreveriZmago()
{
    let racunalnikZ = preveriPetZaporednih("racunalnik", "green");
    if (racunalnikZ) {
        alert("Računalnik je zmagal");
    }
}
function preveriPetZaporednih(tableId, color) {
    let table = document.getElementById(tableId);
    let vrstice = table.querySelectorAll("tr");

    // Check rows
    for (let i = 0; i < vrstice.length; i++) {
        let cells = vrstice[i].querySelectorAll("td");
        let consecutiveCount = 0;
        for (let j = 0; j < cells.length; j++) {
            if (cells[j].style.backgroundColor === color) {
                consecutiveCount++;
                if (consecutiveCount === 5) {
                    return true; // Found 5 consecutive cells
                }
            } else {
                consecutiveCount = 0; // Reset consecutive count if the cell is not the specified color
            }
        }
    }

    // No 5 consecutive cells found
    return false;
}

function  zvok(url)
{
   let audio = document.getElementById("zvok");
   audio.src = url;
   audio.play();
}