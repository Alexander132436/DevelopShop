import { useState } from "react";
//import { useEffect } from "react";
import Card from "./Components/Card/Card";
import Cart from "./Components/Cart/Cart";
import { Cards } from "./Data/Cards";
function App() {
  const[cart,setCart] = useState(false)
  const[cartItem,setCartItem]=useState([])
  // useEffect(()=>{
  //   fetch("http://localhost:5295/products").then((res) => res.json()).then((data) => console.log(data)).catch((error) => console.error('Ошибка:', error));

  // },[]);
 
  function cartHahdler(){
    setCart(!cart);
  }
  function setItemInCart(item){
      setCartItem([...cartItem,item]);
      console.log(cartItem)
  }

  const handleDeleteItem = (id) => {
        console.log('Deleting item:', id);
        setCartItem(cartItem.filter(item => item.Id !== id));
  };


  return (
    <div>
      <div style={{display: 'flex',gap: '10px',alignItems: 'center', flexWrap: 'wrap',minWidth: '450px',justifyContent: 'center'}}>
        {Cards.map(card=>
          <Card key={card.Id} card={card} onClick={setItemInCart} />
        )}        
      </div>
      <button onClick={cartHahdler} >Я корзина</button>
        {cart && <Cart cartItem={cartItem} onDelete={handleDeleteItem}/>}
    </div>
  );
}

export default App;
