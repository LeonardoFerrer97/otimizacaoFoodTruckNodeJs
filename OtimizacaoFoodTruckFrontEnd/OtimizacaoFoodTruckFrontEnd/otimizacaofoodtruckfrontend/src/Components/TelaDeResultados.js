import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import TelaMensagemNaoOtimizacao from './TelaMensagemNaoOtimizacao'
import '../Styles/TelaDeResultados.css'
const styles = theme => ({
  root: {
    width: '100%',
    marginTop: theme.spacing.unit * 3,
    overflowX: 'auto',
  },
  table: {
    minWidth: 700,
  },
});

let id=0;

class TelaDeResultados extends Component {
  createData = (name, quantidadeASerComprado) => {
    id += 1;
    return { id, name, quantidadeASerComprado };
  }

  render() {
    const { classes } = this.props;
    if (this.props.data !== null) {
      console.log(this.props.data)
      let rows = [
        this.createData('Milho', this.props.data.quantidadeMilho),
        this.createData('Pão', this.props.data.quantidadePao),
        this.createData('Bacon', this.props.data.quantidadeBacon),
        this.createData('Batata palha', this.props.data.quantidadeBatataPalha),
        this.createData('Salsicha', this.props.data.quantidadeSalsicha),
        this.createData('Ketchup', this.props.data.quantidadeKetchup),
      ];
      if(rows[0].quantidadeASerComprado === undefined){
        return <TelaMensagemNaoOtimizacao />
      }
      return (
        <div>
        <Paper className={classes.root}>
          <Table className={classes.table}>
            <TableHead>
              <TableRow>
                <TableCell  align="center" >Ingrediente</TableCell>
                <TableCell align="center">Quantidade a ser comprada</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {rows.map(row => (
                <TableRow key={row.id}>
                  <TableCell   align="center" component="th" scope="row">
                    {row.name}
                  </TableCell>
                  <TableCell align="center">{row.quantidadeASerComprado}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </Paper>
        <h1 className={'texto'}>Dinheiro gasto para suprir a demanda seria de {this.props.data.z} reais</h1>
        </div>
      );
    }
    else return <div></div>

  }
}

TelaDeResultados.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(TelaDeResultados);