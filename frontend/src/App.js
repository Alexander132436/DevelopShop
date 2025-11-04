//import { useState } from "react";
import { useEffect } from "react";
import Card from "./Components/Card/Card";
import { Cards } from "./Data/Cards";
function App() {
  useEffect(()=>{
    fetch("http://localhost:5295/products").then((res) => res.json()).then((data) => console.log(data)).catch((error) => console.error('Ошибка:', error));

  },[]);
 
  return (
    <div style={{display: 'flex',gap: '10px',alignItems: 'center', flexWrap: 'wrap',minWidth: '450px',justifyContent: 'center'}}>
      {Cards.map(card=>
        (<Card key={card.Id} card={card}/>)
      )}
      
    </div>
  );
}

export default App;
