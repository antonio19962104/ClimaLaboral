var ganador = false;
function getPosX() {
    return getRandomInt(0, 2)
}
function getPosY() {
    return getRandomInt(0, 2)
}

function getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
}
var tablero = Array(2).fill(null).map(() => Array(4));
function fillTablero(player) {
    var cordX = 0;
    var cordY = 0;
    var marc = "";
    do {
        var coord = tirar(player);
        cordX = parseInt(coord.split('_')[0]);
        cordY = parseInt(coord.split('_')[1]);
        marc = player == 1 ? "X" : "O";
        if (IsNullOrEmpty(document.getElementById("pos_" + cordX + "_" + cordY).value)) {
            tablero[cordX, cordY] = marc;
            document.getElementById("pos_" + cordX + "_" + cordY).value = marc;
            break;
        }
    } while (!IsNullOrEmpty(tablero[cordX, cordY]));
    if (verificaWin(marc, tablero)) {
        console.log("player " + player + " win!");
        ganador = true;
        // iluminate
        var elems = document.getElementsByTagName("input");
        [].forEach.call(elems, function (item) {
            if (item.value == marc)
                item.style.backgroundColor = "rgb(215, 202, 134)";
        });
    }
}
function verificaWin(marc, tablero) {
    // pos_0_0
    document.getElementById("pos_0_0").value;
    if (document.getElementById("pos_0_0").value == marc && document.getElementById("pos_1_0").value == marc && document.getElementById("pos_2_0").value == marc)//***
        return true;
    if (document.getElementById("pos_0_1").value == marc && document.getElementById("pos_1_1").value == marc && document.getElementById("pos_2_1").value == marc)//***
        return true;
    if (document.getElementById("pos_0_2").value == marc && document.getElementById("pos_1_2").value == marc && document.getElementById("pos_2_2").value == marc)//***
        return true;
    if (document.getElementById("pos_0_0").value == marc && document.getElementById("pos_0_1").value == marc && document.getElementById("pos_0_2").value == marc)//v
        return true;
    if (document.getElementById("pos_1_0").value == marc && document.getElementById("pos_1_1").value == marc && document.getElementById("pos_1_2").value == marc)//v
        return true;
    if (document.getElementById("pos_2_0").value == marc && document.getElementById("pos_2_1").value == marc && document.getElementById("pos_2_2").value == marc)//v
        return true;
    if (document.getElementById("pos_0_0").value == marc && document.getElementById("pos_0_1").value == marc && document.getElementById("pos_2_2").value == marc)//d
        return true;
    if (document.getElementById("pos_0_2").value == marc && document.getElementById("pos_1_1").value == marc && document.getElementById("pos_2_0").value == marc)//d
        return true;
    return false;
}
function tirar(player) {
    var x = getPosX();
    var y = getPosY();
    return x + "_" + y;
}
function cat() {
    for (var i = 0; i < 10; i++) {
        if (ganador == false) {
            if (i % 2 == 0)
                fillTablero(1);
            else
                fillTablero(2);
        }
        else {
            var button = document.createElement("button");
            button.innerText = "Limpiar";
            document.body.appendChild(button);
            break;
        }
    }
}
function g() {
    cat();
}
function IsNullOrEmpty(data) {
    if (data == "")
        return true;
    else
        return false;
}

/*
<table>
    <tbody>
        <tr>
            <td><input type="text" id="pos_0_0" disabled /></td>     <td><input type="text" id="pos_1_0" disabled /></td>     <td><input type="text" id="pos_2_0" disabled /></td>
        </tr>
        <tr>
            <td><input type="text" id="pos_0_1" disabled /></td>     <td><input type="text" id="pos_1_1" disabled /></td>     <td><input type="text" id="pos_2_1" disabled /></td>
        </tr>
        <tr>
            <td><input type="text" id="pos_0_2" disabled /></td>     <td><input type="text" id="pos_1_2" disabled /></td>     <td><input type="text" id="pos_2_2" disabled /></td>
        </tr>
    </tbody>
</table>
<input type="button" onclick="cat()" value="de" />
*/