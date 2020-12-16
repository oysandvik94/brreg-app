/**
 * Stable sorting algorithm (from https://material-ui.com/components/tables/#sorting-amp-selecting)
 * 
 * @param array Data to be sorted
 * @param order Sorting order ('asc' or 'desc')
 * @param orderBy Attribute in data to be sorted on
 * @returns Sorted data
 */
export function stableSort(array, order, orderBy) {
    const comparator = getComparator(order, orderBy);
    const stabilizedThis = array.map((el, index) => [el, index]);
    stabilizedThis.sort((a, b) => {
        const order = comparator(a[0], b[0]);
        if (order !== 0) return order;
        return a[1] - b[1];
    });
    return stabilizedThis.map((el) => el[0]);
}

function getComparator(order, orderBy) {
    return order === 'desc'
        ? (a, b) => descendingComparator(a, b, orderBy)
        : (a, b) => -descendingComparator(a, b, orderBy);
}

function descendingComparator(a, b, orderBy) {
    if (b[orderBy] < a[orderBy]) {
        return -1;
    }
    if (b[orderBy] > a[orderBy]) {
        return 1;
    }
    return 0;
}