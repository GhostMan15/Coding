var maxPrijavljenih;
var stPrijavljenih = [0, 0, 0, 0];
var oddelki = ['R4B', 'R4A'];
var skupine = ['B. Resinovič',
    'B. Lubej',  
    'J. Koren', 
    'B. Slemenšek', 
];
var imeSkupin = [
    'Humanoidni robot NAO in Brain Computer Interface',
    'Phyton',
    'Processing',
    'JavaScript'
];
var opisi = [
    "programiranje robota Nao, izdelava programa z uporabo možgansko računalniškega vmesnika",
    "osnovni ukazi in metode za delo, izdelava preproste igre",
    "programiranje proceduralnih animacij in generiranje slik, izdelava interaktivne animacije",
    "JS za postavitev aktivnih strani, izdelava igre"
];
var stSkupin = 4;

function Connect(){
    prejetOdgovor();
}

function prejetOdgovor(){
    maxPrijavljenih = 15;
    stSkupin = 4;/*
    for(i = 0; i < stSkupin; i++){
    //shranimo podatke o prijavljenih
    }*/
    stPrijavljenih[0] = 3;
    stPrijavljenih[1] = 5;
    stPrijavljenih[2] = 12;
    stPrijavljenih[3] = 1;
    //shranimo podatke o oddelkih

} 

function IzpisPodatkov(){
    var izpis = "";
    izpis = `
    <table border='1' class='table_2'>
    <tr allgin = "center">
        <td class="col_15">
            Skupina
        </td>
        <td class="col_15">
            Prijavljeni
        </td>
        <td class="col_35">
            Nosilec
        </td>
        <td class="col_35">
            Tematika
        </td>
    </tr>
    `;
    
    for(var i = 0; i < stSkupin; i++){
        izpis += `
            <td class="col_15">
            ` + (i + 1) + `
            </td>
            <td class="col_15">
            ` + stPrijavljenih[i] + `
            </td>
            <td class="col_35">
            ` +  skupine[i] + `
            </td>
            <td class="col_35">
            ` + imeSkupin[i] +   `
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td colspan="2">` + opisi[i] + `</td>
        </tr>
        `;
    }
    izpis += `</table>`;
    document.getElementById('TabelaSkupin').innerHTML = izpis;
}

function IzpisIzbir(){
    var izbira1 = `<select name='prva' required></select>
        <option value="">-- Prva skupina --</option>
    `;
    var izbira2 = `<select name="druga" required></select>
        <option value="">-- Druga skupina --</option>
    `;
    var options = ``;
    for(var i = 0; i < stSkupin; i++){
        options += `<option value="` + (i + 1) + `">`+ (i + 1) + ` - ` + imeSkupin[i] + `</option>`;
    }
    izbira1 += options + `</select>`;
    izbira2 += options + `</select>`;

    document.getElementById('prva_izbira').innerHTML = izbira1;
    document.getElementById('druga_izbira').innerHTML = izbira2;
}