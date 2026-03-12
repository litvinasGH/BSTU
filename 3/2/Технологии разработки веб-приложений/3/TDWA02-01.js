const url =  '/api/Save-JSON';

function PrintResponse(response){

    let container = document.getElementById("text");

    container.innerHTML = "";

    if(response.error){
        container.innerText = `ERROR ${response.error}: ${response.message}`;
    }
    else if(response.deleted){
         container.innerText = 'DELETED';
    }
    else{
        container.innerText = `RESULT: ${response.op}\nX = ${response.x}\nY = ${response.y}\nRes = ${response.result}`;
    }

}



let request = async (method) => {

  let params = {
    method: method,
    headers: {'content-type':'application/json'}
  }

  if(method == 'POST' || method == 'PUT'){
    params.body = JSON.stringify(GetInput());
  }

  let response = await fetch(url, params);

  let text = await response.text();
  
  let data = text ? JSON.parse(text) : {deleted:true};

  PrintResponse((data));
}


function GetInput(){
    return{
        op: document.getElementById('op').value,
        x: +document.getElementById('X').value,
        y: +document.getElementById('Y').value
    }
}