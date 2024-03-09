let velikost;
let stevilke = [];
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

    podatkiDiv.innerHTML = '';

    if (stVrstic === 3) {
        let tabela = '';
        for (let i = 0; i < 5; i++) {
            tabela += '<table>';
            for (let j = 0; j < 3; j++) {
                tabela += '<tr>';
                for (let stolp = 0;stolp < 5;stolp++) {
                    let number = i * 15 + j * 5 +stolp + 1;
                    if (number <= 75) {
                        tabela += '<td>' + number + '</td>';
                    }
                }
                tabela += '</tr>';
            }
            tabela += '</table>';
        }
        podatkiDiv.innerHTML += tabela;
    } else if (stVrstic === 4) {

        let tabela = '';
        for (let i = 0; i < 5; i++) {
            tabela += '<table>';
            for (let j = 0; j < 4; j++) {
                tabela += '<tr>';
                for (let k = 0; k < 5; k++) {
                    let number = i * 20 + j * 5 + k + 1;
                    if (number <= 80) {
                        tabela += '<td>' + number + '</td>';
                    }
                }
                tabela += '</tr>';
            }
            tabela += '</table>';
        }
        podatkiDiv.innerHTML += tabela;
    } else if (stVrstic === 5) {

        let tabela = '';
        for (let i = 0; i < 5; i++) {
            tabela += '<table>';
            for (let j = 0; j < 5; j++) {
                tabela += '<tr>';
                for (let k = 0; k < 5; k++) {
                    let number = i * 15 + j * 5 + k+ 1;
                    if (number <= 75) {
                        tabela += '<td>' + number + '</td>';
                    }
                }
                tabela += '</tr>';
            }
            tabela += '</table>';
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
}
function racunalnikKopira()
{
    if (izbranaTabela)
    {
        let kloniri = izbranaTabela.cloneNode(true);
        let racunalnikDiv =  document.getElementById("racunalnik");
        let ime = racunalnikDiv.querySelector("h2").cloneNode(true);
        //let button = racunalnikDiv.querySelector("button").cloneNode(true);
        racunalnikDiv.innerHTML = '';
        racunalnikDiv.appendChild(ime);
       // racunalnikDiv.appendChild(button);
        document.getElementById("racunalnik").appendChild(kloniri);
    }
}
//Za prikazovanje in skrivanje tebl
/*function prikaziSkrij() {
    let skrij = document.getElementById("skrij");
    skrij.addEventListener("click", function () {
        let podatkiDiv = document.getElementById("podatki");
        if (podatkiDiv.style.display === "none") {
            podatkiDiv.style.display = "block";
            this.textContent = "Skrij";
        } else {
            podatkiDiv.style.display = "none";
            this.textContent = "Prikaži";
        }
    });
}
document.addEventListener("DOMContentLoaded", function() {
    prikaziSkrij();
});*/

