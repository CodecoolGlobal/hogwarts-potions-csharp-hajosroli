import { useState, useEffect } from "react";


      const createPotion = (potion) => {
        return fetch("http://localhost:5076/api/potions/brew", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(potion),
        })
          .then((res) => res.json())
          .catch((error) => {
            console.error("Error creating potion:", error);
          });
      };

     

      

const BrewingForm = ()=> {
    const [student, setStudent] = useState({})
    const [potions, setPotions] = useState([])
    const [open, setOpen] = useState(false)
    const [ingredient, setIngredient] = useState("")
    const [recipes, setRecipes] = useState([])
    
  

    const handleSubmit =(e)=> {
        e.preventDefault();
        const newPotion = {
            studentId : student
        }
        createPotion(newPotion) 
        .then(() => fetchPotions())
        .then(() => console.log("brew potion was created"))
       }
     
       
       const fetchPotions = async() => {
        const data = await fetch("http://localhost:5076/api/potions/brewPotions")
        const response = await data.json()
        setPotions(response)
    }

    const updatePotion = (ingredient, id) => {
        return fetch(`http://localhost:5076/api/potions/${id}/add`,{
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(ingredient),
        })
          .then((res) => res.json())
    .catch((error) => {
        console.error("Error updating potion:", error);
    });
}
    const getRecipes = async(id) => {
      const data = await fetch(`http://localhost:5076/api/potions/${id}/help`)
      const response = await data.json()
      console.log(response)
      setRecipes(response)
    }

    useEffect(() => {
        fetchPotions()
    }, [])

    
    const openInput =(e)=> {
        e.preventDefault();
        setOpen(!open)
    }

    const addIngredient = (e, id)=>{
      console.log(id)
      e.preventDefault()
        const newIngredient = {
            name: ingredient
        }
        updatePotion(newIngredient, id)
        .then(() => fetchPotions())
        setOpen(false)
    }

    const getHelp = (e, id) => {
      e.preventDefault()
      getRecipes(id)
      .then(()=> console.log(recipes.length))
    }
    return(
       <>
    <form className="PotionForm" onSubmit={handleSubmit}>
  <div className="form-group">
    <label htmlFor="StudentId">Student Id</label>
    <input 
    type="number" 
    className="form-control" 
    id="StudentId" 
    placeholder="1" 
    onChange={e => setStudent(e.target.value)}/>
    <button type="submit">Brew Potion</button>
  </div>
  </form>
  <div>
        <h3>Potions</h3>
        <table>
        <thead>
        <tr>
          <th>Potion Id</th>
          <th>Student Id</th>
          <th>Student Name </th>
          <th>Status</th>
          <th>Ingredients</th>
        </tr>
        </thead>
        <tbody>
            {potions? potions.map(p => (
                <tr key={p.id}>
                    <td>{p.id}</td>
                    <td>{p.student.id}</td>
                    <td>{p.student.name}</td>
                    <td>{p.status}</td>
                    <td><ul>{p.ingredients? p.ingredients.map(i => <li key={i.key}>{i.name}</li>):"no ingredients"}</ul></td>
                    <td key={`add-ingredient-${p.id}`}><button onClick={openInput}>Add ingredient</button></td>
                  {open ? (
                    <td key={`ingredient-input-${p.id}`}>
                      <input
                        name="ingredients"
                        id="ingredients"
                        onChange={e => setIngredient(e.target.value)}
                      />
                      <button onClick={e => addIngredient(e, p.id)}>Add</button>
                    </td>) :(<td key={`empty-${p.id}`}></td>
                        )}
                      <td key={`help-${p.id}`}><button onClick={e => {getHelp(e, p.id)}}>Help</button></td>
                      {recipes ? <td><ul >{recipes.map(r => <li key={r.id}>{r.name}</li>)}</ul></td>: <td></td>}                     
                </tr>
            )): <h1>No potions</h1>}
        </tbody>
        </table>
  </div>
  </>


    )
}
export default BrewingForm;
/*<div class="form-group">
    <label for="exampleFormControlSelect1">Example select</label>
    <select class="form-control" id="exampleFormControlSelect1">
      <option>1</option>
      <option>2</option>
      <option>3</option>
      <option>4</option>
      <option>5</option>
    </select>
  </div>
  <div class="form-group">
    <label for="exampleFormControlSelect2">Example multiple select</label>
    <select multiple class="form-control" id="exampleFormControlSelect2">
      <option>1</option>
      <option>2</option>
      <option>3</option>
      <option>4</option>
      <option>5</option>
    </select>
  </div>
  <div class="form-group">
    <label for="exampleFormControlTextarea1">Example textarea</label>
    <textarea class="form-control" id="exampleFormControlTextarea1" rows="3"></textarea>
  </div>*/