import React, { useState, useEffect } from 'react';
import axios from 'axios'
import { Button, Form, Modal } from 'react-bootstrap';
// import { useAuth0 } from '@auth0/auth0-react'; 
import { useLocalStorage } from '../../CustomHooks/useLocalStorage';
import adminPic from '../../assets/profile-icons/admin.jpg'
import userPic from '../../assets/profile-icons/user.jpg'
import bannedPic from '../../assets/profile-icons/banned.jpg'
import inactivePic from '../../assets/profile-icons/inactive.png'
import  "./Post-styling.css"

export const Post = ({ id }) => {
  const [posts, setPosts] = useState([]);
  // const {user, isLoading} = useAuth0();
  const [user, setuser] = useLocalStorage('user', null)
  const [newPostContent, setNewPostContent] = useState("");
  const [postToEdit, setPostToEdit] = useState({});
  const [editedPostContent, setEditedPostContent] = useState("");
  const [postToDeleteId, setPostToDeleteId] = useState("");
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);

    const fetchPosts = (async () => {
    let response = await axios.get(`https://localhost:44304/api/Posts/thread/${id}`)
    setPosts(response.data)
  })

  const submitNewPost = async () => {
    let newPost = {
        authorUserId: user.userId,
        threadId: id,
        content: newPostContent 
    }

    await axios
    .post(`https://localhost:44304/api/Posts`, newPost)
    .then((res) => {
        console.log("Success: ", res.data);
    })
    .catch((error) => {
        console.error("Error:", error);
    });
    setNewPostContent("");
    fetchPosts();
  };

  const handleCloseEdit = () => setShowEditModal(false);
  
  const handleShowEdit = (post) => {
    setPostToEdit(post);
    setShowEditModal(true);
  } 
  
  const saveEditedPost = async (postId) => {
    
    let editedPost = {
      authorUserId: postToEdit.authorUserId,
      threadId: id,
      content: editedPostContent 
  }
    
    await axios
    .put(`https://localhost:44304/api/Posts?postId=${postId}`, editedPost)
    .catch((error) => {
      console.error("Error:", error);
    });

    fetchPosts();
    setShowEditModal(false);
  }

  const handleCloseDelete = () => setShowDeleteModal(false);
  
  const handleShowDelete = (postId) => {
      setShowDeleteModal(true);
      setPostToDeleteId(postId); 
  }
  
  const deletePost = async (postId) => {
    
      await axios
      .delete(`https://localhost:44304/api/Posts?postId=${postId}`)
      .catch((error) => {
        console.error("Error:", error);
    });

    setPostToDeleteId("");
    setShowDeleteModal(false);
    fetchPosts();

    }

    const toggleCensorPost = async (postId) => {
      await axios
        .put(`https://localhost:44304/censor/${postId}`)
        .catch((error) => {
          console.error("Error:", error);
        });

      fetchPosts();
    }


useEffect(()=>{
    fetchPosts();
  },[])
  
// if(isLoading){
//     return <div> <p>Loading page...</p></div>
//   }
    return <div>

            { !posts && <p>There are no posts on this thread yet...</p> }

            { posts && posts.map((post) => 
                <div className='single-post' key={post.postId}>
                    <div className="avatar-container">
                      {user.roles.includes("admin") && <img className='avatar' src={adminPic} alt="profile picture" />}
                      {user.roles.includes("user") && !post.author.isBanned && post.author.isActive && <img className='avatar' src={userPic} alt="profile picture" />}
                      {post.author.isBanned && post.author.isActive && <img className='avatar' src={bannedPic} alt="profile picture" />}
                      {!post.author.isActive && !post.author.isBanned && <img className='avatar' src={inactivePic} alt="profile picture" />}
                    </div>
                    
                    <div className="post-content">
                        
                        {post.author.isBanned && post.author.isActive && <p className='post-metadata'>Posted by <strong className='post-metadata-bold'>{post.author.userName} &#91;In Jail&#93; </strong> at {post.createdAt}</p>}
                        {!post.author.isActive && !post.author.isBanned && <p className='post-metadata'>Posted by <strong className='post-metadata-bold'>{post.author.userName} &#91;Deceased&#93; </strong> at {post.createdAt}</p>}
                        {!post.author.isBanned && post.author.isActive && <p className='post-metadata'>Posted by <strong className='post-metadata-bold'>{post.author.userName}</strong> at {post.createdAt}</p>}
                        
                        {!post.isCensored && <p>{post.content}</p>}
                        {post.isCensored && <p className='censored-post'>&#91;This mook disrespected the family; their words are silenced.&#93;</p>}
                        
                    </div>
                    
                    <div className="post-buttons">
                        {post.author.userId == user.userId && 
                        <div>
                            <Button className='post-btn' onClick={() => handleShowEdit(post)}><i className='fas fa-edit' title="Edit post"></i></Button>
                            <Button className='post-btn' title='Delete post' onClick={() => handleShowDelete(post.postId)}><i className="fas fa-trash-alt"></i></Button>
                        </div>}
                        {post.author.userId != user.userId && <Button className='post-btn' title='Report post'><i className="fas fa-exclamation"></i></Button>}
                        {user.roles.includes("admin") && (<Button className='post-btn' onClick={() => toggleCensorPost(post.postId)} title='Censor post content'><i className="fas fa-comment-slash"></i></Button>)}
                        
                    </div>
                </div>
            )}

                  <div className='thread-reply'>
                          <Form.Control
                            className="reply-textarea"
                            as="textarea"
                            cols="60" 
                            rows="5"
                            placeholder="Your reply..."
                            value={newPostContent}
                            onChange={(e) => setNewPostContent(e.target.value)}
                          />
                          <Button className="reply-button" onClick={submitNewPost}>Post reply</Button>
                  </div>

                  {/* Delete modal */}
                  <Modal show={showDeleteModal} onHide={handleCloseDelete} className="delete-modal">
                        <Modal.Header closeButton>
                            <Modal.Title>Are you sure you want to delete this post?</Modal.Title>
                        </Modal.Header>
                        <Modal.Footer>
                          <Button variant="secondary" onClick={handleCloseDelete}>
                            Cancel
                          </Button>
                          <Button variant="danger" onClick={() => deletePost(postToDeleteId)}>
                            Delete
                          </Button>
                        </Modal.Footer>
                  </Modal>

                  {/* Edit modal */}
                  <Modal 
                  show={showEditModal} 
                  onHide={handleCloseEdit}
                  size="lg"
                  aria-labelledby="contained-modal-title-vcenter"
                  >
                        <Modal.Header closeButton>
                          <Modal.Title>Edit post</Modal.Title>
                        </Modal.Header>
                          <Form.Control 
                          as="textarea"
                          rows="5"
                          onChange={(e) => setEditedPostContent(e.target.value)}
                          >{postToEdit.content}</Form.Control>
                        <Modal.Footer>
                          <Button variant="secondary" onClick={handleCloseEdit}>
                            Close
                          </Button>
                          <Button variant="primary" onClick={() => saveEditedPost(postToEdit.postId)}>
                            Save Changes
                          </Button>
                        </Modal.Footer>
                  </Modal>
        
        </div>;
};

export default Post;