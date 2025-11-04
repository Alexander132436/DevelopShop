import { useState } from 'react'
import classes from './Card.module.css'
// import foto from '../../Media/Images/apple.webp'
export default function Card({card}){
    const [but,setBut] = useState('Нажми меня')
    return(
        <article className={classes.productCard}>
            <figure className={classes.productCard__image}>
                <img src={card.ImageUrl} alt=""/>
            </figure>
            <div className = {classes.productCard__content}>
                <h3 className={classes.productCard__title}>{card.Name}</h3>
                <p className={classes.productCard__description}>{card.Description}</p>
                <div className={classes.productCard__footer}>
                    <span className={classes.productCard__price}>{card.Price}</span>
                    <button onClick={()=>setBut(but==='Нажми меня' ? 'Нажал' : 'Нажми меня')} className={classes.productCard__button}>{but}</button>
                </div>
            </div>
        </article>
    )
}