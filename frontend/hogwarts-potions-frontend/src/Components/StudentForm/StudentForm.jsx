import { Button, Form, Col, Row } from "react-bootstrap"
import { useEffect, useState } from "react"
import Loading from "../Loading/Loading"
import '../../App.css'



const StudentForm = ({fetchStudents}) => {
    const[houseTypes,setHouseTypes] = useState([])
    const[petTypes, setPetTypes] = useState([])
    const [loading, setLoading] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");
    const[student, setStudent] = useState({
        name: "",
        houseType: "",
        petType: ""
    })

    const createStudent = async (student) => {
        try {
            const response = await fetch("http://localhost:5076/api/Student/addStudent", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(student)
            });
            if (response.status === 409) {
                // Handle conflict (resource already exists) here
                setErrorMessage("Student with this name already exists");
                console.log("Student already exists");
                // You can display an error message to the user, update UI, etc.
            } else if (response.ok) {
                // Handle successful creation here
                const data = await response.json();
                setErrorMessage("");
                console.log("Student created successfully");
                return data;
                // You can show a success message, update UI, etc.
            } else {
                // Handle other status codes or errors
                setErrorMessage("You have to fill all the fields!")
                console.log("Error creating student");
                // Handle the error case, display an error message, etc.
            }
        } catch (error) {
            console.error("Error creating student:", error);
            throw error;
        }
    };
   
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
  
    const fetchHouseTypes = async () => {
        try {
            const response = await fetch("http://localhost:5076/api/student/houseTypes");
            if (!response.ok) {
                throw new Error(`Error fetching house types: ${response.statusText}`);
            }
            const data = await response.json();
            console.log(data)
            setHouseTypes(data);
        } catch (error) {
            console.error("An error occurred while fetching house types:", error);
        }
    };
    
    const fetchPetTypes = async () => {
        try {
            const response = await fetch("http://localhost:5076/api/student/petTypes");
            if (!response.ok) {
                throw new Error(`Error fetching pet types: ${response.statusText}`);
            }
            const data = await response.json();
            setPetTypes(data);
        } catch (error) {
            console.error("An error occurred while fetching pet types:", error);
          
        }
    };
    
    if ( loading) {
        return <Loading />;
      }

    return (
        <div>
            <Form onSubmit={handleSubmit}>
          <Row className="mb-3">
            <Form.Group as={Col} md="4" controlId="student.name">
              <Form.Label>Name:</Form.Label>
              <Form.Control
                required
                type="text"
                placeholder="Name"
                value={student?.name || ""} 
                onChange={e => setStudent({...student, name: e.target.value})}
              />
             {errorMessage && <p className="error"  style={{ color: 'red' }}>{errorMessage}</p>}
            </Form.Group>
            <Form.Group as={Col} md="4" controlId="student.houseType">
            <Form.Label>House Type:</Form.Label>
            <Form.Select 
            required
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
            {errorMessage && <p className="error"  style={{ color: 'red' }}>{errorMessage}</p>}
            </Form.Group>
            <Form.Group as={Col} md="4" controlId="student.petType">
                <Form.Label>Pet Type:</Form.Label>
                <Form.Select 
                required
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
                {errorMessage && <p className="error"  style={{ color: 'red' }}>{errorMessage}</p>}
            </Form.Group>
          </Row>
            <Button type="submit" className="custom-btn">Add Student</Button>
        </Form>
        </div>
        )
}
export default StudentForm;