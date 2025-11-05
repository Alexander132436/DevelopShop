

export default function Cart({cartItem,onDelete}){
    return(
        <div>
            {cartItem.map((el)=>  (
                <div key={el.Id}>
                    <div>{el.Name}</div>
                    <button onClick={()=>onDelete(el.Id)}>Удалить</button>
                
                </div>
            ))}
        </div>
    )
}