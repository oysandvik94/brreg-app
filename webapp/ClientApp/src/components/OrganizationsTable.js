import React, {useEffect, useState} from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import TableSortLabel from "@material-ui/core/TableSortLabel";
import * as PropTypes from "prop-types";
import {stableSort} from "../utils/Sorting";

const useStyles = makeStyles({
    table: {
        minWidth: 650,
    },
    visuallyHidden: {
        border: 0,
        clip: 'rect(0 0 0 0)',
        height: 1,
        margin: -1,
        overflow: 'hidden',
        padding: 0,
        position: 'absolute',
        top: 20,
        width: 1,
    }
});

// Headers and column definition
const headCells = [
    { id: 'orgnr', numeric: true, label: 'Organisasjonsnummer' },
    { id: 'orgname', numeric: false, label: 'Navn/foretaksnavn' },
    { id: 'orgtype', numeric: false, label: 'Organisasjonsform' },
    { id: 'municipality', numeric: false, label: 'Kommune' }
];

// Main method for loading component
export function OrganizationTable(props) {
    const [data, setData] = useState([]);
    const [order, setOrder] = React.useState("asc");
    const [orderBy, setOrderBy] = React.useState("orgnr");
    const classes = useStyles();

    const handleRequestSort = (event, property) => {
        const isAsc = orderBy === property && order === "asc";
        setOrder(isAsc ? 'desc' : 'asc');
        setOrderBy(property);
    };
    
    // Fetch data async
    useEffect(() => {
        fetchData()
            .then(d => {
                setData(d)
            });
    }, [])
    
    // TODO: Make loading screen
    if (data === undefined || data.length === 0) {
        return (
            <p><em>Laster inn...</em></p>        
        );
    }
    
    // Data has been fetched, render table
    return (
       <TableContainer component={Paper}>
           <Table className={classes.table} aria-label="simple table">
               <EnhancedTableHead
                   classes={classes}
                   order={order}
                   orderBy={orderBy}
                   onRequestSort={handleRequestSort}
               />
               <TableBody>
                       {stableSort(data, order, orderBy)
                       .map((row) => {
                           return (
                            <TableRow key={row.orgnr}>
                               {headCells.map((cell) => (
                                   <TableCell
                                       key={cell.id}
                                       component="th"
                                       align={cell.numeric ? 'left' : 'right'}
                                       scope="row"
                                   >
                                       {row[cell.id]}
                                   </TableCell>
                               ))}
                            </TableRow>
                           );
                       })}
               </TableBody>
           </Table>
       </TableContainer>
    );
}

// Custom header element for adding sorting
function EnhancedTableHead(props) {
    const { classes, order, orderBy, onRequestSort } = props;
    const createSortHandler = (property) => (event) => {
        onRequestSort(event, property);
    };

    return (
        <TableHead>
            <TableRow>
                {headCells.map((headCell) => (
                    <TableCell
                        key={headCell.id}
                        align={headCell.numeric ? 'left' : 'right'}
                        sortDirection={orderBy === headCell.id ? order : false}
                    >
                        <TableSortLabel
                            active={orderBy === headCell.id}
                            direction={orderBy === headCell.id ? order : 'asc'}
                            onClick={createSortHandler(headCell.id)}
                        >
                            {headCell.label}
                            {orderBy === headCell.id ? (
                                <span className={classes.visuallyHidden}>
                                  {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
                                </span>
                            ) : null}
                        </TableSortLabel>
                    </TableCell>
                ))}
            </TableRow>
        </TableHead>
    );
}

EnhancedTableHead.propTypes = {
    classes: PropTypes.object.isRequired,
    onRequestSort: PropTypes.func.isRequired,
    order: PropTypes.oneOf(["asc", "desc"]).isRequired,
    orderBy: PropTypes.string.isRequired,
};

async function fetchData() {
    const response = await fetch('organizations');
    return await response.json();
}
