import React, { useState } from 'react';
import { MyButton } from './Button';



export function Counter(){
    const [counter, setCount] = useState<number>(0);


    function inc(){
        setCount(prev => prev + 1);
    
    }
    
    function reset(){
        setCount(0);
    
    }

    return (
<div id = 'container'>
        <div id = 'counterContainer'>
           {counter}
            </div>

        <div id = 'buttonsContainer'>
      
        <MyButton title = 'inc' callback = {inc} disabled = {counter >= 5}/>
        <MyButton title = 'reset' callback = {reset} disabled = {counter == 0}/>
        
      </div>
</div>
        

    );
}