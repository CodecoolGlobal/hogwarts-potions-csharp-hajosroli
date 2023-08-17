import { useState, useEffect } from "react";
import {Button, Table, Form} from "react-bootstrap"
import '../../App.css'

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

      const deletePotion = (id) => {
        return fetch(`http://localhost:5076/api/potions/deletePotion/${id}`, {
          method: "DELETE",
        })
          .then((res) => res.json())
          .catch((error) => {
            console.error("Error deleting potion:", error);
          });
      }

     
const BrewingForm = ()=> {
    const [student, setStudent] = useState({})
    const [potions, setPotions] = useState([])
    const [open, setOpen] = useState(false)
    const [ingredient, setIngredient] = useState("")
    const [recipes, setRecipes] = useState([])
    
    const handleSubmit = async(e)=> {
        e.preventDefault();
        const newPotion = {
            studentId : student
        }
        await createPotion(newPotion) 
        await fetchPotions()
        await console.log("brew potion was created")
       }
     
       const handleDelete = async(e) => {
          await deletePotion(e)
          await fetchPotions()
          console.log("potion was deleted")
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

    const addIngredient = async (e, id)=>{
      console.log(id)
      e.preventDefault()
        const newIngredient = {
            name: ingredient
        }
        await updatePotion(newIngredient, id)
        await fetchPotions()
        setOpen(false)
    }

    const getHelp = async (e, id) => {
      e.preventDefault()
      await getRecipes(id)
     console.log(recipes.length)
    }
    return(
       <>
    <Form className="PotionForm" onSubmit={handleSubmit}>
  <div className="form-group">
    <label htmlFor="StudentId">Student Id</label>
    <input 
    type="number" 
    className="form-control" 
    id="StudentId" 
    placeholder="1" 
    onChange={e => setStudent(e.target.value)}/>
    <Button  type="submit" className="custom-btn">Brew Potion</Button>
  </div>
  </Form>
  <div>
        <h3>Potions</h3>
        <Table striped bordered hover size="sm">
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
                    <td><ul>{p.ingredients? p.ingredients.map(i => <li key={i.id}>{i.name}</li>):"no ingredients"}</ul></td>
                    <td key={`add-ingredient-${p.id}`}><button onClick={openInput}>Add ingredient</button></td>
                  {open ? (
                    <td key={`ingredient-input-${p.id}`}>
                      <input
                        name="ingredients"
                        id="ingredients"
                        onChange={e => setIngredient(e.target.value)}
                      />
                      <Button 
                      variant="success"
                      onClick={e => addIngredient(e, p.id)}>Add</Button>
                    </td>) :(<td key={`empty-${p.id}`}></td>
                        )}
                      <td key={`help-${p.id}`}>
                        <Button 
                        variant="info"
                        onClick={e => {getHelp(e, p.id)}}>Help
                        </Button>
                      </td>
                       <td>
                        {recipes ??<ul>{recipes.map(r => <li key={r.id}>{r.name}</li>)}</ul>}   </td>
                      <td>
                      <>
                      <Button 
                      variant="danger" 
                      onClick={() =>handleDelete(p.id)}>Remove Potion
                      </Button>
                      </>
                    </td>                   
                </tr>
            )): <h1>No potions</h1>}
        </tbody>
        </Table>
  </div>
  </>


    )
}
export default BrewingForm;
