// export function levenshteinDistance(a: string, b: string): number {
//   if (a.length == 0) return b.length;
//   if (b.length == 0) return a.length;

//   var matrix = [];

//   // increment along the first column of each row
//   var i;
//   for (i = 0; i <= b.length; i++) {
//     matrix[i] = [i];
//   }

//   // increment each column in the first row
//   var j;
//   for (j = 0; j <= a.length; j++) {
//     matrix[0][j] = j;
//   }
//   var table = [];

//   // Fill in the rest of the matrix
//   for (i = 1; i <= b.length; i++) {
//     for (j = 1; j <= a.length; j++) {
//       if (b.charAt(i - 1) == a.charAt(j - 1)) {
//         matrix[i][j] = matrix[i - 1][j - 1];
//       } else {
//         matrix[i][j] = Math.min(
//           matrix[i - 1][j - 1] + 1, // substitution
//           Math.min(
//             matrix[i][j - 1] + 1, // insertion
//             matrix[i - 1][j] + 1
//           )
//         ); // deletion
//       }
//     }
//   }

//   console.table(matrix);
//   return matrix[b.length][a.length];
// }

// export function createLevenshteinTable(a: string, b: string): number[][] {
//   var matrix = [];

//   // increment along the first column of each row
//   var i;
//   for (i = 0; i <= b.length; i++) {
//     matrix[i] = [i];
//   }

//   // increment each column in the first row
//   var j;
//   for (j = 0; j <= a.length; j++) {
//     matrix[0][j] = j;
//   }

//   // Fill in the rest of the matrix
//   for (i = 1; i <= b.length; i++) {
//     for (j = 1; j <= a.length; j++) {
//       if (b.charAt(i - 1) == a.charAt(j - 1)) {
//         matrix[i][j] = matrix[i - 1][j - 1];
//       } else {
//         matrix[i][j] = Math.min(
//           matrix[i - 1][j - 1] + 1, // substitution
//           Math.min(
//             matrix[i][j - 1] + 1, // insertion
//             matrix[i - 1][j] + 1
//           )
//         ); // deletion
//       }
//     }
//   }
//   return matrix;
// }

// export function convertLevenshteinTable(a: string, b: string): number[] {
//   const result: number[] = [];
//   const levenshteinTable = createLevenshteinTable(a, b);
//   let i = 1;
//   let j = 1;
//   let currectDistance = 0;

//   while (i < a.length + 1 && j < b.length + 1) {
//     result.push(currectDistance === levenshteinTable[i][j] ? 0 : 1);
//     currectDistance = levenshteinTable[i][j];
//     if (currectDistance === levenshteinTable[i + 1][j + 1]) {
//       i++;
//       j++;
//     } else if (currectDistance === levenshteinTable[i + 1][j]) {
//       i++;
//     } else if (currectDistance === levenshteinTable[i][j + 1]) {
//       j++;
//     }
//   }
//   return result;
// }

export const Equal = 0
export const Replace = 1
export const Delete = 2
export const Insert = 3

type Levenpath = { i: number; j: number; type: number }[]
export type LevenpathResult = { char: string; type: number }
export function levenshtein(str1: string, str2: string): LevenpathResult[] {
  str1 = str1.toLowerCase()
  str2 = str2.toLowerCase()
  const m: number[][] = [],
    paths: [number, number][][] = [],
    l1 = str1.length,
    l2 = str2.length
  for (let i = 0; i <= l1; i++) {
    m[i] = [i]
    paths[i] = [[i - 1, 0]]
  }
  for (let j = 0; j <= l2; j++) {
    m[0][j] = j
    paths[0][j] = [0, j - 1]
  }
  for (let i = 1; i <= l1; i++)
    for (let j = 1; j <= l2; j++) {
      if (str1.charAt(i - 1) === str2.charAt(j - 1)) {
        m[i][j] = m[i - 1][j - 1]
        paths[i][j] = [i - 1, j - 1]
      } else {
        const min = Math.min(m[i - 1][j], m[i][j - 1], m[i - 1][j - 1])
        m[i][j] = min + 1
        if (m[i - 1][j] === min) paths[i][j] = [i - 1, j]
        else if (m[i][j - 1] === min) paths[i][j] = [i, j - 1]
        else if (m[i - 1][j - 1] === min) paths[i][j] = [i - 1, j - 1]
      }
    }

  let levenpath: Levenpath = []
  let j = l2
  for (let i = l1; i >= 0 && j >= 0; )
    for (j = l2; i >= 0 && j >= 0; ) {
      levenpath.push({ i, j, type: Equal })
      const t = i
      i = paths[i][j][0]
      j = paths[t][j][1]
    }
  levenpath = levenpath.reverse()
  for (let i = 0; i < levenpath.length; i++) {
    const last = levenpath[i - 1],
      cur = levenpath[i]
    if (i !== 0) {
      if (cur.i === last.i + 1 && cur.j === last.j + 1 && m[cur.i][cur.j] !== m[last.i][last.j])
        cur.type = Replace
      // "replace";
      else if (cur.i === last.i && cur.j === last.j + 1) cur.type = Insert
      // "insert";
      else if (cur.i === last.i + 1 && cur.j === last.j) cur.type = Delete // "delete";
    } else {
      cur.type = 0
    }
  }

  const result = levenpath.map((cur, i) => {
    switch (cur.type) {
      case Replace:
        return { char: str1.charAt(cur.i - 1), type: Replace } as LevenpathResult
      case Insert:
        return { char: str2.charAt(cur.j - 1), type: Insert } as LevenpathResult
      case Delete:
        return { char: str1.charAt(cur.i - 1), type: Delete } as LevenpathResult
      default:
        return { char: str1.charAt(cur.i - 1), type: Equal } as LevenpathResult
    }
  })
  return result.slice(1)
}
