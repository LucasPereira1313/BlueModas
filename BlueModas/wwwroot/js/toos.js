//Send to server
function sendToApi(data) {

    if (data != null) {

        if (data.hasOwnProperty('methodo') &&
            data.hasOwnProperty('url') &&
            data.hasOwnProperty('datajson')) {

            $.ajax({
                type: String(data['methodo']),
                url: String(data['url']),
                data: String(data['datajson']),
                contentType: 'application/json',
                dataType: 'json',
                success: function (response) {
                    let msg = {
                        title: 'Salvo',
                        messagem: 'Itens salvo com sucesso.',
                    };

                    if (data.hasOwnProperty('returnmessage')) {
                        if (String(data['returnmessage']).toLowerCase() == 'true') {
                            messagemShow(msg);
                        }
                    }

                    if (data.hasOwnProperty('command')) {
                        let execut = data['command'];
                        execut(response);
                    }

                },
                failure: function (response) {
                    let msg = {
                        title: 'Falha',
                        messagem: response.d,
                    };

                    messagemShow(msg);
                },
                error: function (response) {
                    let msg = {
                        title: 'Erro',
                        messagem: response.d,
                    };

                    messagemShow(msg);
                }
            });
        }
        else {
            let msg = {
                title: 'Erro',
                messagem: 'Falta parametros.',
            };

            messagemShow(msg);
        }
    }
    else {
        let msg = {
            title: 'Erro',
            messagem: 'Valor nulo a ser ennviado.',
        };

        messagemShow(msg);
    }
}

//Check element empty.
function emptyCheck(element) {
    if (element != null) {
        if (element.value.length > 0) {
            return true
        }
    }
    return false;
}

//Show message to user.
function messagemShow(value) {

    if (value != null) {

        if (value.hasOwnProperty('title') &&
            value.hasOwnProperty('messagem')) {

            document.getElementById('modal_messagem_label').innerText = value['title'];
            document.getElementById('modal_messagem_text').innerText = value['messagem'];

            $("#modal_messagem").modal();
        }
    }
}

//Generate Guid
function createGuid() {
    function S4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }
    return (S4() + S4() + "-" + S4() + "-4" + S4().substr(0, 3) + "-" + S4() + "-" + S4() + S4() + S4()).toLowerCase();
} 

//Generate new guid
function identfyUser() {
    let cokkies = document.cookie;
    let name = 'BlueModas';

    if (cokkies.length > 0) {

        let itens = cokkies.split('=');

        if (itens.length > 0) {

            if (String(itens[0]) == name) {

                return String(itens[1]);

            }
        }
    }

    let guid = createGuid();
    document.cookie = name + '=' + String(guid);
    return String(guid);
}

//Open or create new user in database
function startUser() {
    let data = {
        methodo: 'GET',
        url: '/api/Carrinho/GetQtd' + '?guid=' + identfyUser(),
        datajson: null,
        returnmessage: false,
        command: function (response) {
            showQtdProduto(response);
        },
    };

    sendToApi(data)
}

//Show number of product
function showQtdProduto(qtd) {
    let content = document.getElementById('carrinho_qtd');

    if (content != null) {

        if (qtd != null) {
            content.innerText = String(qtd);
        }
        else {
            content.innerText = '0';
        }
    }
}

//Add products on car
function addProdutoCarrinho(id) {
    let data = {
        methodo: 'POST',
        url: '/api/Carrinho/AddProduto' + '?guid=' + identfyUser() + '&produto=' + String(id),
        datajson: null,
        returnmessage: false,
        command: function (response) {
            showQtdProduto(response);
        },
    };

    sendToApi(data)
}

//Get product
function listProduct(id) {

    if (id != null) {
        let data = {
            methodo: 'GET',
            url: '/api/Produto/GetListId' + '?categoria=' + String(id),
            datajson: null,
            returnmessage: false,
            command: function (response) {
                loadHome(response);
            },
        };

        sendToApi(data);
    }
    else {
        let data = {
            methodo: 'GET',
            url: '/api/Produto/GetList',
            datajson: null,
            returnmessage: false,
            command: function (response) {
                loadHome(response);
            },
        };

        sendToApi(data);
    }
}

//Load list of product
function loadHome(data) {

    if (data != null) {

        let content = document.getElementById('main_list');

        if (content != null) {

            content.innerHTML = '';

            if (data != null) {

                for (var i = 0; i < data.length; i++) {

                    let iten = data[i];
                    let id = iten['id'];
                    let descricao = iten['descricao'];
                    let observacao = iten['observacao'];
                    let categoria = iten['categoria'];
                    let image = iten['imageBase64'];
                    let valor = iten['valor'];
                    let desconto = iten['desconto'];
                    let currency_valor = valor.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' });


                    let element_i = document.createElement("button");
                    element_i.type = 'button';
                    element_i.className = 'btn btn-primary btn-sm btn-block';
                    element_i.onclick = function () { addProdutoCarrinho(id); };
                    element_i.innerHTML = '<svg width="20px" height="20px" viewBox="0 0 16 16" class="bi bi-cart4 mr-2" fill="currentColor" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M0 2.5A.5.5 0 0 1 .5 2H2a.5.5 0 0 1 .485.379L2.89 4H14.5a.5.5 0 0 1 .485.621l-1.5 6A.5.5 0 0 1 13 11H4a.5.5 0 0 1-.485-.379L1.61 3H.5a.5.5 0 0 1-.5-.5zM3.14 5l.5 2H5V5H3.14zM6 5v2h2V5H6zm3 0v2h2V5H9zm3 0v2h1.36l.5-2H12zm1.11 3H12v2h.61l.5-2zM11 8H9v2h2V8zM8 8H6v2h2V8zM5 8H3.89l.5 2H5V8zm0 5a1 1 0 1 0 0 2 1 1 0 0 0 0-2zm-2 1a2 2 0 1 1 4 0 2 2 0 0 1-4 0zm9-1a1 1 0 1 0 0 2 1 1 0 0 0 0-2zm-2 1a2 2 0 1 1 4 0 2 2 0 0 1-4 0z" /></svg>Adicionar';
                    

                    let element_h = document.createElement("p");
                    element_h.className = 'mb-2';
                    element_h.innerText = String(observacao);

                    let element_g = document.createElement("h3");
                    element_g.className = 'mb-0';
                    element_g.innerText = String(descricao);

                    let element_f = document.createElement("strong");
                    element_f.className = 'd-inline-block mb-2 text-danger';
                    element_f.innerText = String(currency_valor);

                    let element_e = document.createElement("div");
                    element_e.className = 'col p-4 d-flex flex-column position-static';
                    element_e.appendChild(element_f);
                    element_e.appendChild(element_g);
                    element_e.appendChild(element_h);
                    element_e.appendChild(element_i);

                    let element_d = document.createElement("img"); 
                    element_d.className = 'img-fluid';
                    element_d.src = image;

                    let element_c = document.createElement("div"); 
                    element_c.className = 'col-auto d-none d-lg-block';
                    element_c.appendChild(element_d);

                    let element_b= document.createElement("div");
                    element_b.className = 'row no-gutters border rounded overflow-hidden flex-md-row mb-4 shadow-sm h-md-250 position-relative';
                    element_b.appendChild(element_e);
                    element_b.appendChild(element_c);

                    let element_a = document.createElement("div"); 
                    element_a.className = 'col-md-6';
                    element_a.appendChild(element_b);

                    content.appendChild(element_a);

                }
            }
        }
    }
}


function getShopping() {
    let data = {
        methodo: 'GET',
        url: '/api/Carrinho/GetShopping' + '?guid=' + identfyUser(),
        datajson: null,
        returnmessage: false,
        command: function (response) {
            loadShopping(response);
        },
    };

    sendToApi(data)
}


//Load list of product on car
function loadShopping(data) {

    if (data != null) {

        let content = document.getElementById('main_shopping');

        if (content != null) {

            content.innerHTML = '';
            let subtotal = 0;

            if (data != null) {

                let element_h6 = document.createElement("h6");
                element_h6.className = 'border-bottom border-gray pb-2 mb-0';
                element_h6.innerText = 'Itens do carrinho';

                for (var i = 0; i < data.length; i++) {

                    let iten = data[i];
                    let contagem = iten['contagem'];
                    let valortotal = iten['valorTotal'];
                    let produto = iten['produto'];

                    let image = produto['imageBase64'];
                    let valor = produto['valor'];
                    let descricao = produto['descricao'];


                    subtotal = subtotal + valortotal;

                    let currency_valorunitario = valor.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' });
                    let currency_valortotak = valortotal.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' });                    


                    let element_i = document.createElement("button");
                    element_i.type = 'button';
                    element_i.className = 'btn btn-primary btn-sm btn-block';
                    element_i.innerHTML = '<svg width="20px" height="20px" viewBox="0 0 16 16" class="bi bi-cart4 mr-2" fill="currentColor" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M0 2.5A.5.5 0 0 1 .5 2H2a.5.5 0 0 1 .485.379L2.89 4H14.5a.5.5 0 0 1 .485.621l-1.5 6A.5.5 0 0 1 13 11H4a.5.5 0 0 1-.485-.379L1.61 3H.5a.5.5 0 0 1-.5-.5zM3.14 5l.5 2H5V5H3.14zM6 5v2h2V5H6zm3 0v2h2V5H9zm3 0v2h1.36l.5-2H12zm1.11 3H12v2h.61l.5-2zM11 8H9v2h2V8zM8 8H6v2h2V8zM5 8H3.89l.5 2H5V8zm0 5a1 1 0 1 0 0 2 1 1 0 0 0 0-2zm-2 1a2 2 0 1 1 4 0 2 2 0 0 1-4 0zm9-1a1 1 0 1 0 0 2 1 1 0 0 0 0-2zm-2 1a2 2 0 1 1 4 0 2 2 0 0 1-4 0z" /></svg>Adicionar';


                    let element_h = document.createElement("label");
                    element_h.innerText = 'Total ' + String(currency_valortotak);


                    let element_g = document.createElement("strong");
                    element_g.className = 'text-gray-dark';
                    element_g.innerText = 'Valor unitário ' + String(currency_valorunitario); 


                    let element_f = document.createElement("div");
                    element_f.className = 'd-flex justify-content-between align-items-center w-100';
                    element_f.appendChild(element_g);
                    element_f.appendChild(element_h);


                    let element_e = document.createElement("span");
                    element_e.className = 'd-block';
                    element_e.innerText = String(contagem) + 'x - ' + String(descricao);


                    let element_d = document.createElement("div");
                    element_d.className = 'media-body pb-3 mb-0 small lh-125 border-bottom border-gray';
                    element_d.appendChild(element_f);
                    element_d.appendChild(element_e);
                                                           

                    let element_image = document.createElement("img");
                    element_image.className = 'bd-placeholder-img mr-2 rounded';
                    element_image.src = image;
                    element_image.style = 'height: 50px; width: auto;';


                    let element_a = document.createElement("div");
                    element_a.className = 'media text-muted pt-3';
                    element_a.appendChild(element_image);
                    element_a.appendChild(element_d);

                    content.appendChild(element_a);                    

                }

                let currency_valorsubtotal = subtotal.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' });

                let element_small = document.createElement("h6");
                element_small.className = 'd-block text-right mt-3';
                element_small.innerText = 'Total a pagar ' + String(currency_valorsubtotal);

                content.appendChild(element_small);

                

            }
        }
    }
}
