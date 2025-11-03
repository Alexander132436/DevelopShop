import classes from './Card.module.css'
export default function Card(){
    return(
        <article className={classes.productCard}>
            <figure className={classes.productCard__image}>
                <img src="" alt=""/>
            </figure>
            <dir className = {classes.productCard__content}>
                <h3 className={classes.productCard__title}>Название товара</h3>
                <p className={classes.productCard__description}>Краткое описание товара</p>
                <div className={classes.productCard__footer}>
                    <span className={classes.productCard__price}>1200p</span>
                    <button className={classes.productCard__button}>В корзину</button>
                </div>
            </dir>
        </article>
    )
}