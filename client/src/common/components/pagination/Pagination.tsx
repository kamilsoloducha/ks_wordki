import { useCallback, useEffect, useState } from "react";
import { PageChangedEvent } from "./pageChagnedEvent";
import "./Pagination.scss";

export function Pagination({
  totalCount,
  onPageChagned,
  search,
  onSearchChanged,
  pageSize = 30,
}: Model) {
  const [currectPage, setCurrectPage] = useState(1);
  const totalPages = Math.ceil(totalCount / pageSize);
  const buttons = getPagesToDispaly(totalPages, currectPage);

  const changePage = useCallback(
    (page: number) => {
      if (page <= 0 || page > totalPages || page === currectPage) return;
      setCurrectPage(page);
      onPageChagned({
        currectPage: page,
        count: pageSize,
        first: (page - 1) * pageSize + 1,
      });
    },
    [setCurrectPage, onPageChagned, totalPages, currectPage, pageSize]
  );

  const onSearchTextChanged = useCallback(
    (event: any) => {
      const text = event.target.value;
      if (onSearchChanged) {
        onSearchChanged(text);
      }
    },
    [onSearchChanged]
  );

  useEffect(() => {
    changePage(1);
  }, [totalCount, pageSize]); // eslint-disable-line

  return (
    <div className="pagination-container">
      <div className="pagination-left">
        {onSearchChanged && (
          <input type="search" placeholder="Search" value={search} onChange={onSearchTextChanged} />
        )}
      </div>
      <div className="pagination-middle">
        <div
          className={`button ${currectPage === 1 ? "disabled" : ""}`}
          onClick={() => changePage(1)}
        >
          &lt;&lt;
        </div>
        <div
          className={`button ${currectPage === 1 ? "disabled" : ""}`}
          onClick={() => changePage(currectPage - 1)}
        >
          &lt;
        </div>
        {buttons.map((x) => {
          return (
            <div
              className={`button ${currectPage === x ? "selected" : ""}`}
              key={x}
              onClick={() => changePage(x)}
            >
              {x}
            </div>
          );
        })}
        <div
          className={`button ${currectPage === totalPages ? "disabled" : ""}`}
          onClick={() => changePage(currectPage + 1)}
        >
          &gt;
        </div>
        <div
          className={`button ${currectPage === totalPages ? "disabled" : ""}`}
          onClick={() => changePage(totalPages)}
        >
          &gt;&gt;
        </div>
      </div>
      <div className="pagination-right"></div>
    </div>
  );
}

interface Model {
  totalCount: number;
  pageSize?: number;
  onPageChagned: (event: PageChangedEvent) => void;
  search?: string;
  onSearchChanged?: (text: string) => void;
}

function getPagesToDispaly(totalPages: number, currectPage: number): number[] {
  const result: number[] = [];
  const factor = 3;
  for (let i = currectPage - factor; i <= currectPage + factor; i++) {
    result.push(i);
  }
  return result.filter((x) => x > 0 && x <= totalPages);
}
