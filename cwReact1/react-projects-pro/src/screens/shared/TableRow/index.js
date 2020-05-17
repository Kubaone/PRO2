import React from "react";

export default function TableRow({ user, setSelectedUser, selectedUser }) {
  return (
    <tr
      style={{
        backgroundColor:
          user.idUser == selectedUser.idUser ? "yellow" : "white",
      }}
      onClick={() => setSelectedUser(user)}
    >
      <td>{user.idUser}</td>
      <td>{user.firstName}</td>
      <td>{user.lastName}</td>
    </tr>
  );
}
