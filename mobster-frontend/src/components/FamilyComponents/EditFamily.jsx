import axios from "axios";
import React, { useState, useContext, useEffect } from "react";
import { useParams } from "react-router-dom";
import { Button, Form, FloatingLabel, Modal, Alert } from "react-bootstrap";
import { Context } from "../../utils/store";
import { Typeahead } from "react-bootstrap-typeahead";
import "react-bootstrap-typeahead/css/Typeahead.css";

const EditFamily = () => {
  const [context, updateContext] = useContext(Context);
  const [success, setSuccess] = useState(false);
  const [error, setError] = useState(false);
  const [showModal, setShowModal] = useState(false);
  const [selected, setSelected] = useState([]);

  //useState with object, how to partial update object
  //https://stackoverflow.com/questions/54150783/react-hooks-usestate-with-object
  const [familyName, setfamilyName] = useState("");
  const [description, setDescription] = useState("");
  const { id } = useParams();

  useEffect(() => {
    fetchFamily();
  }, []);

  const fetchFamily = async () => {
    const response = await axios.get(
      `https://localhost:44304/api/Family/${id}`
    );
    setfamilyName(response.data.name);
    setDescription(response.data.description);
    updateContext({
      currentAdmin: {
        userName: response.data.adminName,
        userId: response.data.adminUserId,
      },
    });
  };

  const onSubmit = async () => {
    let updatedFamily = {
      Name: familyName,
      Description: description,
      AdminId: context.currentAdmin.userId,
    };
    await axios
      .put(`https://localhost:44304/api/Family?familyId=${id}`, updatedFamily)
      .then((res) => {
        console.log("Success: ", res.data);
        setSuccess(true);
      })
      .catch((error) => {
        console.error("Error:", error);
        setError(true);
      });
    //how to update parent component without refreshing page? context?

    const timer = setTimeout(() => {
      window.location.reload(false);
    }, 2000);
  };

  const onSelectAdmin = (data) => {
    setSelected(data);
  };
  const handleClose = () => {
    updateContext({ currentAdmin: selected[0] });
    setShowModal(false);
  };

  const handleShow = async () => {
    const response2 = await axios.get(
      `https://localhost:44304/api/Family/${id}/members`
    );
    updateContext({ familyMembers: response2.data });
    setShowModal(true);
  };

  return (
    <>
      <div>
        {success && (
          <Alert variant="success">Success! Update in process...</Alert>
        )}
        {error && <Alert variant="danger">Error. Please try again</Alert>}
        <FloatingLabel
          controlId="familyNameLabel"
          label="Family name"
          className="mb-3"
        >
          <Form.Control
            as="textarea"
            value={familyName}
            onChange={(e) => setfamilyName(e.target.value)}
          />
        </FloatingLabel>
        <FloatingLabel controlId="familyDescription" label="Description">
          <Form.Control
            as="textarea"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            style={{ height: "100px" }}
          />
        </FloatingLabel>
        <p>Current admin: {context.currentAdmin.userName}</p>
        <Button variant="success" onClick={handleShow}>
          Change admin
        </Button>
        <Button onClick={onSubmit}>Save changes</Button>

        <Modal show={showModal} onHide={handleClose}>
          <Modal.Header closeButton>
            <Modal.Title>Change admin</Modal.Title>
          </Modal.Header>

          <Modal.Body>
            <div>
              <Form.Group>
                <Form.Label>Find user</Form.Label>
                <Typeahead
                  id="basic-typeahead-single"
                  labelKey="userName"
                  onChange={(e) => onSelectAdmin(e)}
                  options={context.familyMembers}
                  placeholder="Type an username"
                  selected={selected}
                />
              </Form.Group>
            </div>
          </Modal.Body>
          <Modal.Footer>
            <Button onClick={handleClose}>Select</Button>
          </Modal.Footer>
        </Modal>
      </div>
    </>
  );
};

export default EditFamily;
