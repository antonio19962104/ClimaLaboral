    function getPosX()
    {
        return getRandomInt(0, 2)
    }
    function getPosY()
    {
        return getRandomInt(0, 2)
    }

    function getRandomInt(min, max) {
        min = Math.ceil(min);
        max = Math.floor(max);
        return Math.floor(Math.random() * (max - min + 1)) + min;
    }
    var tablero = Array(2).fill(null).map(() => Array(4));
    function fillTablero(player)
    {
        var cordX = 0;
        var cordY = 0;
        var marc = "";
        do
        {
            var coord = tirar(player);
            cordX = parseInt(coord.split('_')[0]);
            cordY = parseInt(coord.split('_')[1]);
            marc = player == 1 ? "X" : "O";
            if (IsNullOrEmpty(tablero[cordX, cordY]))
            {
                tablero[cordX, cordY] = marc;
                document.getElementsByClassName("pos_" + cordX + "_" + cordY)[0].value = marc;
                break;
            }
        } while (!IsNullOrEmpty(tablero[cordX, cordY]));
        if (verificaWin(marc, tablero)){
            console.log("p: " + player + " win");
            for (var i = 0; i < 5; i++){
            	for (var j = 0; j < 5; j++) {
            		console.log(tablero[i,j]);
            	}
            }
        }
    }
    function verificaWin(marc, tablero)
    {
        if (tablero[0, 0] == marc && tablero[1, 0] == marc && tablero[2, 0] == marc)//***
            return true;
        if (tablero[0, 1] == marc && tablero[1, 1] == marc && tablero[2, 1] == marc)//***
            return true;
        if (tablero[0, 2] == marc && tablero[1, 2] == marc && tablero[2, 2] == marc)//***
            return true;
        if (tablero[0, 0] == marc && tablero[0, 1] == marc && tablero[0, 2] == marc)//v
            return true;
        if (tablero[1, 0] == marc && tablero[1, 1] == marc && tablero[1, 2] == marc)//v
            return true;
        if (tablero[2, 0] == marc && tablero[2, 1] == marc && tablero[2, 2] == marc)//v
            return true;
        if (tablero[0, 0] == marc && tablero[0, 1] == marc && tablero[2, 2] == marc)//d
        	return true;
    	if (tablero[0, 2] == marc && tablero[1, 1] == marc && tablero[2, 0] == marc)//d
    		return true;
        return false;
    }
    function tirar(player)
    {
        var x = getPosX();
        var y = getPosY();
        return x + "_" + y;
    }
    function cat()
    {
        for (var i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
                fillTablero(1);
            else
                fillTablero(2);
        }
    }
    function g()
    {
        cat();
    }
    function IsNullOrEmpty(data){
        if (data == null || data == undefined || data == "" || data != "X" || data != "O")
            return true;
        else
            return false;
    }
/*
table input {
    width: 30px;
    text-align: center;
}
<table>
    <tbody>
        <tr>
            <td><input type="text" class="pos_0_0" disabled /></td>     <td><input type="text" class="pos_1_0" disabled /></td>     <td><input type="text" class="pos_2_0" disabled /></td>
        </tr>
        <tr>
            <td><input type="text" class="pos_0_1" disabled /></td>     <td><input type="text" class="pos_1_1" disabled /></td>     <td><input type="text" class="pos_2_1" disabled /></td>
        </tr>
        <tr>
            <td><input type="text" class="pos_0_2" disabled /></td>     <td><input type="text" class="pos_1_2" disabled /></td>     <td><input type="text" class="pos_2_2" disabled /></td>
        </tr>
    </tbody>
</table>
<input type="button" onclick="cat()" value="de" />
*/