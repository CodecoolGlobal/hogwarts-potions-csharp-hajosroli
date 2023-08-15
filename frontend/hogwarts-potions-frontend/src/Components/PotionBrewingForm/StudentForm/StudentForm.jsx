import { Button, Form, Col, Row } from "react-bootstrap"
import { useEffect, useState } from "react"
import Loading from "../../Loading/Loading"

const createStudent = async (student) => {
    try {
        const response = await fetch("http://localhost:5076/api/Student/addStudent", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(student)
        });

        const data = await response.json();
        return data;
    } catch (error) {
        console.error("Error creating student:", error);
        throw error;
    }
};

const StudentForm = ({fetchStudents}) => {
    const[houseTypes,setHouseTypes] = useState([])
    const[petTypes, setPetTypes] = useState([])
    const [loading, setLoading] = useState(false);
    const[student, setStudent] = useState({
        name: "",
        houseType: "",
        petType: ""
    })
   
    const handleSubmit = async(e) => {
        e.preventDefault();
        await createStudent(student)
        await fetchStudents()
        e.target.reset();
        setStudent({
            name: "",
            houseType: "",
            petType: ""
        }); 
      };

      useEffect(() => {
        const fetchData = async () => {
            try {
                setLoading(true);
                await fetchHouseTypes();
                await fetchPetTypes();
                setLoading(false);
            } catch (error) {
                console.error("Error fetching data:", error);
                setLoading(false);
            }
        };
        fetchData();
    }, []);
  
    const fetchHouseTypes = async() => {
        const data = await fetch("http://localhost:5076/api/student/houseTypes")
        const response = await data.json()
        setHouseTypes(response)
    }
    const fetchPetTypes = async() => {
        const data = await fetch("http://localhost:5076/api/student/petTypes")
        const response = await data.json()
        setPetTypes(response)
    }

    if ( loading) {
        return <Loading />;
      }
      
    return (
        <div>
            <Form onSubmit={handleSubmit}>
          <Row className="mb-3">
            <Form.Group as={Col} md="4" controlId="student.name">
              <Form.Label>First name</Form.Label>
              <Form.Control
                required
                type="text"
                placeholder="Name"
                value={student?.name || ""} 
                onChange={e => setStudent({...student, name: e.target.value})}
              />
              <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
            </Form.Group>
            <Form.Group as={Col} md="4" controlId="student.houseType">
            <Form.Select 
            aria-label="Default select example" 
            value={student?.houseType || ""}  
            onChange={(e)=>setStudent({ ...student , houseType: e.target.value})}
            >           
                <option>Open this select menu</option>
                {houseTypes.map((value) => (
                    <option key={value} value={value}>
                        {value}
                    </option>
                ))}
            </Form.Select>
            </Form.Group>
          </Row>
          <Row className="mb-3">
          <Form.Group as={Col} md="4" controlId="student.petType">
            <Form.Select 
            aria-label="Default select example"
            value={student?.petType || ""}
            onChange={(e)=>setStudent({ ...student , petType: e.target.value})}
            >           
                <option>Open this select menu</option>
                {petTypes.map((value) => (
                    <option key={value} value={value}>
                        {value}
                    </option>
                ))}
            </Form.Select>
            </Form.Group>
          </Row>
          <Button type="submit">Submit form</Button>
        </Form>
            </div>
        )
}
export default StudentForm;