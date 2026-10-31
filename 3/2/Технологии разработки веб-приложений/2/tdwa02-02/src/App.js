import './App.css';
import { useState } from 'react';

function App() {
  let [op, setOp] = useState('add');
  let [x, setX] = useState(0);
  let [y, setY] = useState(0);
  let [responseText, setResponseText] = useState("");

const url = '/api/Save-JSON';


function formatResult(message){
  if(message.error){
    return `ERROR ${message.error}: ${message.message}`;
  }

  else if(message.deleted){
    return 'DELETED';
  }

  return `RESULT: ${message.op}\nX = ${message.x}\nY = ${message.y}\nRes = ${message.result}`;

}

let request = async (method) => {

  let params = {
    method: method,
    headers: {'content-type':'application/json'}
  }

  if(method === 'POST' || method === 'PUT'){
    params.body = JSON.stringify({op:op, x:+x, y:+y});
  }

  let response = await fetch(url, params);

  let text = await response.text();
  console.log('debug: ', text);

  let data = text ? JSON.parse(text) : {deleted:true};

  setResponseText(formatResult(data));
}


  return (
    <div className="App">
      <div id="responseContainer">
        <pre id="text">{responseText}</pre>
      </div>
    <button className="MethodButton" id="GetButton" onClick={() => request('GET')}>Get</button>
<br></br>


        <label>operation:  <select name="op" id="op" onChange={e => setOp(e.target.value)}>
            <option value="add">+</option>
            <option value="sub">-</option>
            <option value="div">/</option>
            <option value="mul">*</option>
        </select><br/></label><br/>
       
        <label>x: <input type="number" id="X" value={x} onChange={e => setX(e.target.value)}/><br/></label><br/>
        
        <label>Y: <input type="number" id="Y" value={y} onChange={e => setY(e.target.value)}/><br/></label><br/>
        


    <button className="MethodButton" id="PostButton" onClick={() => request('POST')}>Post</button>

    <button className="MethodButton" id="PutButton" onClick={() => request('PUT')}>Put</button>

    <button className="MethodButton" id="DeleteButton" onClick={() => request('DELETE')}>Delete</button>


    </div>
  );
}

export default App;