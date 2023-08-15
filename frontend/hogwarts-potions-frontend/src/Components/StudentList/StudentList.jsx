import { useEffect, useState } from "react"
import Button from "react-bootstrap"
import StudentForm from "../PotionBrewingForm/StudentForm/StudentForm";

const deleteStudent = async (id) => {
  try {
      const response = await fetch(`http://localhost:5076/api/student/deleteStudent/${id}`, {
          method: "DELETE"
      });

      const data = await response.json();
      console.log(data);
  } catch (error) {
      console.error("Error deleting student:", error);
  }
};
const StudentList = () => {

    const[students, setStudents] = useState([])
    
    const fetchStudents = async () => {
      try {
          const data = await fetch("http://localhost:5076/api/student/getAllStudents");
          const response = await data.json();
          setStudents(response);
      } catch (error) {
          console.error("Error fetching students:", error);
      }
  };

  const handleDelete = async (id) => {
      await deleteStudent(id)
      await fetchStudents()
    .catch((err) => console.error(err));
  }

    useEffect(() => {
        fetchStudents()
        .then(()=> console.log("students are fetched"))
    }, [])

    return(
        <>
        <StudentForm
        fetchStudents ={fetchStudents}
        />
        <table>
          <tbody>
          {students.map((student) =>  (
                  <tr key={student.id}>
                    <td>{student.id}</td>
                    <td>{student.name}</td>
                    <td>{student.houseType}</td>
                    <td>{student.petType}</td>
                    <td>
                    <button variant="danger" onClick={() =>handleDelete(student.id)}>
                      Remove Student
                    </button>
                    </td>
                  </tr>
            ))}
          </tbody>
        </table>
            
  
        </>
    )
}
export default StudentList;