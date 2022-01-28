import React from "react";
import { useState, useContext, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import axios from "axios";
import { Button, Table} from "react-bootstrap";

const Members = () => {
  const [members, setmembers] = useState([]);
  const [isLoading, setisLoading] = useState(true);
  
  let navigate = useNavigate();
  
  const { familyId } = useParams();

  useEffect(() => {
    if(familyId) fetchFamily();
  }, [members]);
  
  const fetchFamily = async () => {
    const response = await axios.get(
      `https://localhost:44304/api/Family/${familyId}/members`
    );
    setmembers(response.data);
    setisLoading(false)
  };

  const onBlock = async (member) => {
    let blockedUser = {
      FamilyId: familyId,
      UserId: member.userId,
      Description: "",
    };

    const response = await axios
      .post(`https://localhost:44304/api/Block/`, blockedUser)
      .then((res) => {
        console.log("Success: ", res.data);
      })
      .catch((error) => {
        console.error("Error:", error);
      });
  };

  const onRemove = async (member) => {
    const response = await axios
    .delete(`https://localhost:44304/removeUser?familyId=${familyId}&userId=${member.userId}`)
    .then((res) => {
      console.log("Success: ", res.data);
    })
    .catch((error) => {
      console.error("Error:", error);
    });
  };

  if(isLoading) return <>Loading...</>

  return (
    <div className="container">
      <div className="flex space">
      <h1>List of members({members.length})</h1>
      <Button
        variant="dark"
        onClick={() => navigate(`/family/${familyId}/blockedMembers`)}
        >
        Blocked member list
      </Button>
        </div>
      {/* {Array.from(members).map((member) => (
        <Member key={member.userId} member={member} familyId={familyId} />
      ))} */}
      <Table striped bordered hover size="sm">
        <thead>
          <tr>
            <th>User Name</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {Array.from(members).map((member) => (
            <tr key={member.userId}>
              <td>{member.userName}</td>
              <td>      
                <Button onClick={() => onBlock(member)}>Block</Button>
                <Button variant="danger" onClick={() => onRemove(member)}>Remove</Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </div>
  );
};

export default Members;