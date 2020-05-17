import React from "react";

function TableHeader({ columnsNames }) {
  return (
    <thead>
      <tr>
        {columnsNames.map((c) => (
          <th key={c}>{c}</th>
        ))}
      </tr>
    </thead>
  );
}

export default TableHeader;
