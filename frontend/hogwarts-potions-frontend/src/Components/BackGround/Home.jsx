import image from './img/hogwarts-background.jpeg'
import "./Home.css"


const BackGround = () => (
    <div>
        <img src={image} className="hogwarts-bg" alt="hogwarts-bg" />
    </div>
)

export default BackGround;