////const poke_container = document.getElementById('poke_container');
////const pokemons_number = 10;
////const colors = {
////	fire: '#FDDFDF',
////	grass: '#DEFDE0',
////	electric: '#FCF7DE',
////	water: '#DEF3FD',
////	ground: '#f4e7da',
////	rock: '#d5d5d4',
////	fairy: '#fceaff',
////	poison: '#98d7a5',
////	bug: '#f8d5a3',
////	dragon: '#97b3e6',
////	psychic: '#eaeda1',
////	flying: '#F5F5F5',
////	fighting: '#E6E0D4',
////	normal: '#F5F5F5'
////};
////const main_types = Object.keys(colors);

////const fetchPokemons = async () => {
////	for (let i = 1; i <= pokemons_number; i++) {
////		await getPokemon(i);
////	}
////};

////const getPokemon = async id => {
////	const url = `https://pokeapi.co/api/v2/pokemon/${id}`;
////	const res = await fetch(url);
////	const pokemon = await res.json();
////	createPokemonCard(pokemon);
////};

////function createPokemonCard(pokemon) {
////	const pokemonEl = document.createElement('div');
////	pokemonEl.classList.add('pokemon');

////	const poke_types = pokemon.types.map(type => type.type.name);
////	const type = main_types.find(type => poke_types.indexOf(type) > -1);
////	const name = pokemon.name[0].toUpperCase() + pokemon.name.slice(1);
////	const color = colors[type];

////	pokemonEl.style.backgroundColor = color;

////	const pokeInnerHTML = `
////        <div class="img-container">
////            <img src="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/dream-world/${pokemon.id
////		}.svg" alt="${name}" />
////        </div>
////        <div class="info">
////            <span class="number">#${pokemon.id
////			.toString()
////			.padStart(3, '0')}</span>
////            <h3 class="name">${name}</h3>
////            <small class="type">Type: <span>${type}</span></small>
////        </div>
////    `;

////	pokemonEl.innerHTML = pokeInnerHTML;

////	poke_container.appendChild(pokemonEl);
////}

////fetchPokemons();




function getPokeTypes(PokeUrl) {
    let pokeTypes = [];
    $.ajax({
        url: PokeUrl,
        async: false
    }).done((result) => {
        //pokeTypes = result.types[0].type.name;
        for (var i = 0; i < result.types.length; i++) {
            pokeTypes.push(result.types[i].type.name);
        }
    }).fail((err) => {
        console.log(err);
    })

    return pokeTypes;
}

function getPokeDesc(PokeUrl) {
    var pokeDesc = "";
    $.ajax({
        url: PokeUrl,
        async: false
    }).done((result) => {
        pokeDesc = result.flavor_text_entries[8].flavor_text;

    }).fail((err) => {
        console.log(err);
    })

    return pokeDesc;
}
function getPokeSpecies(PokeUrl) {
    var pokeDesc = "";
    $.ajax({
        url: PokeUrl,
        async: false
    }).done((result) => {
        pokeDesc = result.genera[7].genus;

    }).fail((err) => {
        console.log(err);
    })

    return pokeDesc;
}
function getPokeEvoChainUrl(PokeUrl) {
    var pokeEvoChainUrl = "";
    $.ajax({
        url: PokeUrl,
        async: false
    }).done((result) => {
        pokeEvoChainUrl = result.evolution_chain.url;

    }).fail((err) => {
        console.log(err);
    })

    return pokeEvoChainUrl;
}
function getPokeEvoChain(PokeEvoChainUrl) {
    var pokeEvoChain1 = "";
    var pokeEvoChain2 = "";
    var pokeEvoChain3 = "";
    var reqLv2 = "";
    var reqLv3 = "";
    const arrEvoChain = [pokeEvoChain1, reqLv2, pokeEvoChain2, reqLv3, pokeEvoChain3];

    $.ajax({
        url: PokeEvoChainUrl,
        async: false
    }).done((result) => {
        pokeEvoChain1 = result.chain.species.name;
        reqLv2 = result.chain.evolves_to[0].evolution_details[0].min_level;
        pokeEvoChain2 = result.chain.evolves_to[0].species.name;
        //CEK KALO ADA EVO 3 nya
        if (result.chain.evolves_to[0].evolves_to[0] != undefined) {
            reqLv3 = result.chain.evolves_to[0].evolves_to[0].evolution_details[0].min_level;
            pokeEvoChain3 = result.chain.evolves_to[0].evolves_to[0].species.name;
        }
        

        arrEvoChain[0] = pokeEvoChain1;
        arrEvoChain[1] = reqLv2;
        arrEvoChain[2] = pokeEvoChain2;
        arrEvoChain[3] = reqLv3;
        arrEvoChain[4] = pokeEvoChain3;

    }).fail((err) => {
        console.log(err);
    })

    return arrEvoChain;
}
function getPokeSprites (poke) {
    var pokeSpritesUrl = "";
    $.ajax({
        url: "https://pokeapi.co/api/v2/pokemon/" + poke,
        async: false
    }).done((result) => {
        pokeSpritesUrl = result.sprites.other.dream_world.front_default;

    }).fail((err) => {
        console.log(err);
    })

    return pokeSpritesUrl;
}

//AJAX TABLE HOME
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/?offset=0&limit=31"
    }).done((result) => {
        var text = "";
        var types = "";
        $.each(result.results, function (key, val) {
            types = getPokeTypes(val.url);
            var textTypes = ``;
            for (var i = 0; i < types.length; i++) {
                textTypes += `<span class="badge badge-primary" style="margin-right:5%;background-color:${colors[types[i]]};color:black">${types[i]}</span>`;
            }

            text += `<tr>
                        <td>${key + 1}</td>
                        <td>${val.name}</td>
                        <td>${textTypes}</td>
                        <td>
                            <button type="button"
                                class="btn btn-primary"
                                data-toggle="modal"
                                data-target="#exampleModal"
                                onclick="showDetail('${val.url}')"
                            >
                              Info
                            </button>
                        </td>
                    </tr>`;
        })
        $('#listPoke').html(text);
    }).fail((err) => {
        console.log(err);
    })

const colors = {
	fire: '#FDDFDF',
    grass: '#98d7a5',
	electric: '#FCF7DE',
	water: '#DEF3FD',
    ground: '#b3a65d',
	rock: '#d5d5d4',
	fairy: '#fceaff',
    poison: '#e6c7ff',
	bug: '#f8d5a3',
	dragon: '#97b3e6',
	psychic: '#eaeda1',
    flying: '#d4d4d4',
	fighting: '#E6E0D4',
	normal: '#F5F5F5'
};

function showDetail(detailUrl) {
    $.ajax({
        url: detailUrl
    }).done((result) => {
        var imgName = result.species.name;
        var imgResult = result.sprites.other.dream_world.front_default;
        var imgType = result.types[0].type.name;
        var imgBgColor = colors[imgType];
        var PokeDescUrl = result.species.url;

        var speciesPokemon = getPokeSpecies(PokeDescUrl);
        var heightPokemon = result.height/10;
        var weightPokemon = result.weight / 10;
        var abilitiesPokemon = result.abilities;

        var textAbilities = `<p style="margin-bottom:0px">`;
        for (var i = 0; i < abilitiesPokemon.length; i++) {
            var abilities = abilitiesPokemon[i].ability.name;
            if (i == abilitiesPokemon.length - 1) {
                textAbilities += `${abilities}`;
            }
            else {
                textAbilities += `${abilities}</br>`;
            }
            
        }
        textAbilities += `</p>`;


        $('#namePoke').html('<h1 id="glow" >' + imgName + '</h1 >');
        var PokeDesc = getPokeDesc(PokeDescUrl);
        $('#descPoke').html('<p class="font-italic">"' + PokeDesc + '"</p>');
        $('#imagePoke').html('<img id="cardPoke" src="' + imgResult + '" class="img-fluid " style="background-color: ' + imgBgColor + ';" />');
        $('#speciesPoke').html('<P>' + speciesPokemon +'</p>');
        $('#heightPoke').html('<p >' + heightPokemon +' m </p>');
        $('#weightPoke').html('<p >' + weightPokemon + ' Kg </p>');
        $('#AbilitiesPoke').html(textAbilities);

        var hp = result.stats[0].base_stat;
        var atk = result.stats[1].base_stat ;
        var def = result.stats[2].base_stat ;
        var spd = result.stats[5].base_stat ;
        var speAtk = result.stats[3].base_stat ;
        var speDef = result.stats[4].base_stat ;
        var totalStat = hp + atk + def + spd + speAtk + speDef;


        $('#hpPoke').css({ width: hp + "%" });
        $('#hpPoke').html(hp);
        $('#atkPoke').css({ width: atk + "%"});
        $('#atkPoke').html(atk);
        $('#defPoke').css({ width: def + "%"});
        $('#defPoke').html(def);
        $('#spdPoke').css({ width: spd + "%"});
        $('#spdPoke').html(spd);
        $('#spAtkPoke').css({ width: speAtk + "%"});
        $('#spAtkPoke').html(speAtk);
        $('#spDefPoke').css({ width: speDef + "%"});
        $('#spDefPoke').html(speDef);
        $('#totalStatPoke').html('<h4 class="h4 text-black" style="text-align:center;font-weight:900;letter-spacing: 3px;font-size:25pt">' + totalStat+'</h4>');

        var evoChainUrl = getPokeEvoChainUrl(PokeDescUrl)
        var evoChain = getPokeEvoChain(evoChainUrl);
        var evo1img = getPokeSprites(evoChain[0]);
        var evo2img = getPokeSprites(evoChain[2]);

        $('#pokeEvo1').html('<img id="cardEvoPoke" src="' + evo1img + '" class="img-fluid " /><h5 style="color:black;text-align:center">' + evoChain[0] + '</h5 > ');
        $('#pokeReqEvo2').html('<span style="margin-top:5%;font-size:65pt">&#8594;</span><h5 style="color:black;text-align:center">Level ' + evoChain[1] + '</h5 > ');
        $('#pokeEvo2').html('<img id="cardEvoPoke" src="' + evo2img + '" class="img-fluid " /><h5 style="color:black;text-align:center">' + evoChain[2] + '</h5 > ');
        //CEK ADA GAK EVO 3 nya
        if (evoChain[4] != "") {
            var evo3img = getPokeSprites(evoChain[4]);
            $('#pokeReqEvo3').html('<span style="margin-top:5%;font-size:65pt">&#8594;</span><h5 style="color:black;text-align:center">Level ' + evoChain[3] + '</h5 > ');
            $('#pokeEvo3').html('<img id="cardEvoPoke" src="' + evo3img + '" class="img-fluid " /><h5 style="color:black;text-align:center">' + evoChain[4] + '</h5 > ');
        }
        else {
            $('#pokeReqEvo3').html('');
            $('#pokeEvo3').html('');
        }


        


        //RADAR CHART
        //var marksCanvas = document.getElementById("marksChart");

        //var marksData = {
        //    labels: ["Health", "Attack", "Defense", "Special-Attack", "Special-Defense", "Speed"],
        //    datasets: [{
        //        label: imgName,
        //        backgroundColor: "rgba(200,0,0,0.2)",
        //        data: [65, 75, 70, 80, 60, 80]
        //    }]
        //};

        //var radarChart = new Chart(marksCanvas, {
        //    type: 'radar',
        //    data: marksData,
        //    options: {
        //        legend: {
        //            display: false
        //        }
        //    }
        //});
        //END RADAR CHART

    }).fail((err) => {
        console.log(err);
    })

}